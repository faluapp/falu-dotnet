using Falu;
using Falu.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using System;
using System.Net;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> relating to <see cref="FaluClient"/>
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
            if (configuration is not null)
            {
                services.Configure<FaluClientOptions>(configuration);
            }

            if (configure != null)
            {
                services.Configure(configure);
            }

            // register post configuration for validation purposes
            services.AddSingleton<IPostConfigureOptions<FaluClientOptions>, FaluClientOptionsPostConfigureOptions>();

            // get the version from the assembly
            var productVersion = typeof(FaluClient).Assembly.GetName().Version.ToString(3);

            // setup client
            var builder = services.AddHttpClient<FaluClient>(name: "NewFaluClient" /* TODO: remove this name once the clients migrate from the old FaluClient */)
                                  .ConfigureHttpClient((provider, client) =>
                                  {
                                      // set the base address
                                      client.BaseAddress = new Uri("https://api.falu.io/");

                                      // prepare User-Agent value
                                      var userAgent = $"falu-dotnet/{productVersion}";
                                      var options = provider.GetRequiredService<IOptions<FaluClientOptions>>().Value;
                                      if (options.Application is not null)
                                      {
                                          userAgent += $" {options.Application}";
                                      }

                                      // populate the User-Agent header
                                      client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                                  });

            // setup retries
            builder.AddPolicyHandler((provider, request) =>
            {
                var options = provider.GetRequiredService<IOptions<FaluClientOptions>>().Value;
                var delays = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(0.5f),
                                                                 retryCount: options.Retries);
                var policy = HttpPolicyExtensions.HandleTransientHttpError()
                                                 .OrResult(r => r.StatusCode is HttpStatusCode.BadGateway or HttpStatusCode.ServiceUnavailable /* 502,503 */)
                                                 .WaitAndRetryAsync<System.Net.Http.HttpResponseMessage>(delays);
                return policy;
            });

            // continue configuring the IHttpClientBuilder
            configureBuilder?.Invoke(builder);

            return services;
        }
    }
}
