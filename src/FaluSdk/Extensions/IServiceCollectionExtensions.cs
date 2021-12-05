using Falu;
using Falu.Core;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using System.Net;
using System.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/> relating to <see cref="FaluClient"/> and <see cref="FaluClient{TOptions}"/>
/// </summary>
public static partial class IServiceCollectionExtensions
{
    internal const string Attempts = "attempts";

    /// <summary>
    /// Add client for Falu API
    /// </summary>
    /// <param name="services">the collection to be added to</param>
    /// <param name="apiKey"></param>
    /// <returns></returns>
    public static IHttpClientBuilder AddFalu(this IServiceCollection services, string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey));
        }

        return services.AddFalu(o => o.ApiKey = apiKey);
    }

    /// <summary>
    /// Add client for Falu API
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to be added to.</param>
    /// <param name="configure">An <see cref="Action{FaluClientOptions}"/> to configure the client options.</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddFalu(this IServiceCollection services, Action<FaluClientOptions>? configure = null)
    {
        return services.AddFalu<FaluClient, FaluClientOptions>(configure);
    }

    /// <summary>
    /// Add client for Falu API
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <typeparam name="TClientOptions"></typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to be added to.</param>
    /// <param name="configure">An <see cref="Action{FaluClientOptions}"/> to configure the client options.</param>
    /// <returns></returns>
    public static IHttpClientBuilder AddFalu<TClient, TClientOptions>(this IServiceCollection services, Action<TClientOptions>? configure = null)
        where TClient : FaluClient<TClientOptions>
        where TClientOptions : FaluClientOptions
    {
        /*
         * Binding options from an IConfiguration instance is not used to reduce dependencies and leave that to the caller.
         * Should the implementor/user require this, they can add it using:
         * services.AddFalu(options => Configuration.GetSection("Falu").Bind(options));
         */

        if (configure != null)
        {
            services.Configure(configure);
        }

        // register post configuration for validation purposes
        services.AddSingleton<IPostConfigureOptions<TClientOptions>, PostConfigureFaluClientOptions<TClientOptions>>();

        // get the version from the assembly
        var productVersion = typeof(TClient).Assembly.GetName().Version!.ToString(3);

        // setup client
        var builder = services.AddHttpClient<TClient>()
                              .ConfigureHttpClient((provider, client) =>
                              {
                                      // set the base address
                                      client.BaseAddress = new Uri("https://api.falu.io/");

                                      // populate the User-Agent value for the SDK/library
                                      client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("falu-dotnet", productVersion));
                              });

        // setup retries
        builder.AddPolicyHandler((sp, request) =>
        {
                // Using scope otherwise the IOptionsSnapshot<T> instance will be singleton, never changing
                using var scope = sp.CreateScope();
            var provider = scope.ServiceProvider;
            var options = provider.GetRequiredService<IOptionsSnapshot<TClientOptions>>().Value;
            var delays = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(0.5f),
                                                             retryCount: options.Retries);

                // Network failures are handled via HttpRequestException, other errors are handled in the ShouldRetry method
                var generalRetryPolicy = GetGeneralRetryPolicy(delays);

                // Server has returned a header telling us when to retry if we're making too many requests
                var retryAfterPolicy = GetRetryAfterPolicy(options.Retries);

            return Policy.WrapAsync(generalRetryPolicy, retryAfterPolicy);
        });

        return builder;
    }

    internal static AsyncRetryPolicy<HttpResponseMessage> GetRetryAfterPolicy(int retryCount)
    {
        var policy = Policy.HandleResult<HttpResponseMessage>(r => r?.Headers?.RetryAfter != null)
                           .WaitAndRetryAsync(retryCount: retryCount,
                                              sleepDurationProvider: GetServerWaitDuration,
                                              onRetryAsync: (result, timeSpan, attempts, context) =>
                                              {
                                                      // Include the attempts in the context, thus can be accessed to log events for example
                                                      context[Attempts] = attempts;

                                                      // We could also add any logs for diagnosis here
                                                      return Task.CompletedTask;
                                              });

        return policy;
    }

    internal static AsyncRetryPolicy<HttpResponseMessage> GetGeneralRetryPolicy(IEnumerable<TimeSpan> delays)
    {
        var policy = Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                                                .OrResult(ShouldRetry)
                                                .WaitAndRetryAsync(sleepDurations: delays,
                                                                   onRetryAsync: (result, timeSpan, attempts, context) =>
                                                                   {
                                                                           // Include the attempts in the context, thus can be accessed to log events for example
                                                                           context[Attempts] = attempts;

                                                                           // We could also add any logs for diagnosis here
                                                                           return Task.CompletedTask;
                                                                   });

        return policy;
    }

    internal static bool ShouldRetry(HttpResponseMessage response)
    {
        if (response is null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        // Respect server ask to not retry.
        if (response.Headers.TryGetValues(HeadersNames.XShouldRetry, out var values)
            && bool.TryParse(values.First(), out var b))
        {
            return b;
        }

        // Retry on conflict and timeout errors.
        if (response.StatusCode is HttpStatusCode.Conflict or HttpStatusCode.RequestTimeout)
        {
            return true;
        }

        // Retry on 500, 503, and other internal errors.
        return (int)response.StatusCode >= 500;
    }

    private static TimeSpan GetServerWaitDuration(int retryCount,
                                                  DelegateResult<HttpResponseMessage> response,
                                                  Context context)
    {
        var retryAfter = response?.Result?.Headers?.RetryAfter;
        if (retryAfter == null)
            return TimeSpan.Zero;

        return retryAfter.Date.HasValue ? retryAfter.Date.Value - DateTimeOffset.UtcNow
                                        : retryAfter.Delta.GetValueOrDefault(TimeSpan.Zero);
    }
}
