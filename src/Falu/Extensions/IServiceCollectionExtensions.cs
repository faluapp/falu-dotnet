using Falu;
using Falu.Infrastructure;
using System;
using System.Net.Http.Headers;

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
        /// <param name="services">The <see cref="IServiceCollection"/> to be added to.</param>
        /// <param name="configure">An <see cref="Action{FaluClientOptions}"/> to configure the client options.</param>
        /// <param name="configureBuilder">An <see cref="Action{IHttpClientBuilder}"/> to configure the HTTP client builder.</param>
        /// <returns></returns>
        public static IServiceCollection AddFalu(this IServiceCollection services,
                                                 Action<FaluClientOptions> configure = null,
                                                 Action<IHttpClientBuilder> configureBuilder = null)
        {
            if (configure != null) services.Configure(configure);
            services.PostConfigure<FaluClientOptions>(o =>
            {
                if (string.IsNullOrWhiteSpace(o.ApiKey))
                {
                    var message = "Your API key is invalid, as it is an empty string. You can "
                                + "double-check your API key from the Falu Dashboard. See "
                                + "https://docs.falu.io/api/authentication for details or contact support "
                                + "at https://falu.com/support/email if you have any questions.";
                    throw new FaluException(message);
                }
            });

            // get the version from the assembly
            var productVersion = typeof(FaluClient).Assembly.GetName().Version.ToString(3);

            var builder = services.AddHttpClient<FaluClient>()
                                  .ConfigureHttpClient((provider, client) =>
                                  {
                                      // set the base address
                                      client.BaseAddress = new Uri("https://api.falu.io/");

                                      // populate the User-Agent header
                                      var userAgent = new ProductInfoHeaderValue("falu-dotnet", productVersion);
                                      client.DefaultRequestHeaders.UserAgent.Add(userAgent);
                                  });

            // continue configuring the IHttpClientBuilder
            configureBuilder?.Invoke(builder);

            return services;
        }

        /// <summary>
        /// Add client for Falu API
        /// </summary>
        /// <param name="services">the collection to be added to</param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public static IServiceCollection AddFalu(this IServiceCollection services, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentNullException(nameof(apiKey));
            return services.AddFalu(o => o.ApiKey = apiKey);
        }
    }
}
