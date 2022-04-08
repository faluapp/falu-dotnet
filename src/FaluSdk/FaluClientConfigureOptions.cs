using Microsoft.Extensions.Options;

namespace Falu;

internal class FaluClientConfigureOptions<TClientOptions> : IPostConfigureOptions<TClientOptions>, IValidateOptions<TClientOptions>
    where TClientOptions : FaluClientOptions
{
    public void PostConfigure(string name, TClientOptions options)
    {
        // intentionally left blank for future use
    }

    public ValidateOptionsResult Validate(string name, TClientOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            var message = "Your API key is invalid, as it is an empty string. You can "
                        + "double-check your API key from the Falu Dashboard. See "
                        + "https://docs.falu.io/api/authentication for details or contact support "
                        + "at https://falu.com/support/email if you have any questions.";
            return ValidateOptionsResult.Fail(message);
        }

        if (options.Retries < 0)
        {
            return ValidateOptionsResult.Fail("Retries cannot be negative.");
        }

        return ValidateOptionsResult.Success;
    }
}
