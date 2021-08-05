using Falu.Infrastructure;
using Microsoft.Extensions.Options;

namespace Falu
{
    internal class FaluClientOptionsPostConfigureOptions : IPostConfigureOptions<FaluClientOptions>
    {
        public void PostConfigure(string name, FaluClientOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                var message = "Your API key is invalid, as it is an empty string. You can "
                            + "double-check your API key from the Falu Dashboard. See "
                            + "https://docs.falu.io/api/authentication for details or contact support "
                            + "at https://falu.com/support/email if you have any questions.";
                throw new FaluException(message);
            }

            if (options.Retries < 0)
            {
                throw new FaluException("Retries cannot be negative.");
            }
        }
    }
}
