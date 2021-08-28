using Falu;
using Falu.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> relating to <see cref="FaluClient"/> and <see cref="FaluClient{TOptions}"/>
    /// </summary>
    public static partial class IServiceCollectionExtensions
    {
        /// <summary>
        /// Add client for Falu API
        /// </summary>
        /// <param name="services">the collection to be added to</param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static IServiceCollection AddFalu(this IServiceCollection services, string apiKey)
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
        public static IServiceCollection AddFalu(this IServiceCollection services, Action<FaluClientOptions> configure)
        {
            if (configure is null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            return services.AddFalu(configuration: null, configure: configure, configureBuilder: null);
        }

        /// <summary>
        /// Add client for Falu API
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to be added to.</param>
        /// <param name="configuration">
        /// An <see cref="IConfiguration"/> containing the values of <see cref="FaluClientOptions"/> at its root.
        /// </param>
        /// <returns></returns>
        public static IServiceCollection AddFalu(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return services.AddFalu(configuration: configuration, configure: null, configureBuilder: null);
        }

        /// <summary>
        /// Add client for Falu API
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to be added to.</param>
        /// <param name="configuration">
        /// An <see cref="IConfiguration"/> containing the values of <see cref="FaluClientOptions"/> at its root.
        /// </param>
        /// <param name="configure">An <see cref="Action{FaluClientOptions}"/> to configure the client options.</param>
        /// <param name="configureBuilder">An <see cref="Action{IHttpClientBuilder}"/> to configure the HTTP client builder.</param>
        /// <returns></returns>
        public static IServiceCollection AddFalu(this IServiceCollection services,
                                                 IConfiguration? configuration = null,
                                                 Action<FaluClientOptions>? configure = null,
                                                 Action<IHttpClientBuilder>? configureBuilder = null)
        {
            return services.AddFaluInner<FaluClient, FaluClientOptions>(configuration: configuration,
                                                                        configure: configure,
                                                                        configureBuilder: configureBuilder);
        }

        /// <summary>
        /// Add client for Falu API
        /// </summary>
        /// <typeparam name="TClient"></typeparam>
        /// <typeparam name="TClientOptions"></typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to be added to.</param>
        /// <param name="configuration">
        /// An <see cref="IConfiguration"/> containing the values of <typeparamref name="TClientOptions"/> at its root.
        /// </param>
        /// <param name="configure">An <see cref="Action{FaluClientOptions}"/> to configure the client options.</param>
        /// <param name="configureBuilder">An <see cref="Action{IHttpClientBuilder}"/> to configure the HTTP client builder.</param>
        /// <returns></returns>
        public static IServiceCollection AddFaluInner<TClient, TClientOptions>(this IServiceCollection services,
                                                                               IConfiguration? configuration = null,
                                                                               Action<TClientOptions>? configure = null,
                                                                               Action<IHttpClientBuilder>? configureBuilder = null)
            where TClient : FaluClient<TClientOptions>
            where TClientOptions : FaluClientOptions
        {
            if (configuration is not null)
            {
                services.Configure<TClientOptions>(configuration);
            }

            if (configure != null)
            {
                services.Configure(configure);
            }

            // register post configuration for validation purposes
            services.AddSingleton<IPostConfigureOptions<TClientOptions>, PostConfigureFaluClientOptions<TClientOptions>>();

            // get the version from the assembly
            var productVersion = typeof(TClient).Assembly.GetName().Version.ToString(3);

            // setup client
            var builder = services.AddHttpClient<TClient>()
                                  .ConfigureHttpClient((provider, client) =>
                                  {
                                      // set the base address
                                      client.BaseAddress = new Uri("https://api.falu.io/");

                                      // populate the User-Agent value for the SDK/library
                                      client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("falu-dotnet", productVersion));

                                      // populate the User-Agent for 3rd party providers
                                      var options = provider.GetRequiredService<IOptions<TClientOptions>>().Value;
                                      if (options.Application is not null)
                                      {
                                          var userAgent = options.Application.ToString();
                                          client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                                      }
                                  });

            // setup retries
            builder.AddPolicyHandler((provider, request) =>
            {
                var options = provider.GetRequiredService<IOptions<TClientOptions>>().Value;
                var delays = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(0.5f),
                                                                 retryCount: options.Retries);

                // Network failures are handled via HttpRequestException, other errors are handled in the ShouldRetry method
                var policy = Policy<HttpResponseMessage>.Handle<HttpRequestException>()
                                                        .OrResult(ShouldRetry)
                                                        .WaitAndRetryAsync(delays);
                return policy;
            });

            // continue configuring the IHttpClientBuilder
            configureBuilder?.Invoke(builder);

            return services;
        }

        private static bool ShouldRetry(HttpResponseMessage response)
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
    }
}
