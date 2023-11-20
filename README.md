<a href="https://www.falu.io">
    <img src="falu-logo.png" alt="Falu Logo" title="Falu" width="120" height="120" align="right">
</a>

# Falu .NET

[![NuGet](https://img.shields.io/nuget/v/Falu.svg)](https://www.nuget.org/packages/Falu/)
[![GitHub Workflow Status](https://github.com/faluapp/falu-dotnet/actions/workflows/build.yml/badge.svg)](https://github.com/faluapp/falu-dotnet/actions/workflows/build.yml)

The official [Falu][falu] .NET library, supporting .NET Standard 2.1+.

## Installation

Using the [.NET Core command-line interface (CLI) tools][dotnet-core-cli-tools]:

```sh
dotnet add package Falu
```

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install Falu
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package Falu
```

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "Falu".
5. Click on the `Falu` package, select the appropriate version in the right-tab and click *Install*.

## Documentation

For a comprehensive list of examples, check out the [API
documentation][api-docs].

## Usage

### Simple instance

You can create an instance of the client manually as shown below:

```cs
public class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "<put-you-key-here>";
        var options = Options.Create(new FaluClientOptions { ApiKey = apiKey, });

        var client = new FaluClient(new HttpClient(), options);
        // .... do whatever you wish
    }
}
```

### Dependency Injection

Often it is recommended that you make use of an IoC container to control the lifetime of dependencies created. This is the case in ASP.NET Core but can also be done in background jobs. First, you would put the API Key in your secret:

```jsonc
{
    "Falu": {
        "ApiKey": "<put-you-key-here>",
        "Retries": 3, // defaults to 2
    }
}
```

To setup the secret via cli, use the command:

```console
dotnet secrets add "Falu:ApiKey" "<put-you-key-here>"
```

Next add the client to you instance of `IServicesCollection` in Startup.cs or Program.cs:

```cs
public Startup(IConfiguration configuration)
{
   Configuration = configuration;
}

public IConfiguration Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
   // Add client
   services.AddFalu(Configuration["Falu:ApiKey"]);

   // The sample service we'll use to demonstrate usage
   // It is recommended the service be consumed in a service with scoped or transient lifetime and not in a singleton one
   // If you have to consume in a singleton service, inject an instance of IServiceProvider and get an instance from there when needed
   services.AddScoped<MyService>();
   ....
}
```

Our sample service would then make use of the `FaluClient` as follows:

```cs
private readonly FaluClient client;
public MyService(FaluClient client)
{
   this.client = client ?? throw new ArgumentNullException(nameof(client));
}

public async Task DoSomethingAsync(CancellationToken cancellationToken = default)
{
    // utilize as shown in the simple example above
}
```

### Per-request configuration

All of the service methods accept an optional [idempotency key][idempotency-keys].

```c#
var request = new IdentityVerificationCreateRequest
{
    // omitted for brevity
};
var ro = new RequestOptions { IdempotencyKey = "SOME STRING", };
await client.IdentityVerifications.CreateAsync(request: request, options: ro);
```

## Identity

With `FaluClient` you can verify your user's identity from a documentation perspective. Below is a sample of how to do verify the user's phone number against a name or id card.

```cs
FaluClient client; // omitted for brevity

var request = new IdentityVerificationCreateRequest
{
    Type = "document", // can also be "id_number" or "document_and_selfie" or "document_and_video"
    Options = new IdentityVerificationOptions
    {
        AllowUploads = true,
        Document = new IdentityVerificationOptionsForDocument { },
    },
    ReturnUrl = "https://my-app.com/verify/waiting?userId=123456",
};
var response = await client.IdentityVerifications.CreateAsync(request);
response.EnsureSuccess(); // might throw an exception (FaluException)
var url = response.Resource.Url; // use this URL to complete verification
var clientSecret = response.Resource.ClientSecret; // pass this to Android/iOS apps using the official SDK
```

## Messages

With `FaluClient` you can send both transactional and bulk messages to customers. You can use pre-created templates to streamline sending of your messages. Below is a sample of how to create a template then used it to send a message.

```cs
FaluClient client; // omitted for brevity

// Creating message template (only needs to be done once)
await client.MessageTemplates.CreateAsync(new TemplatePatchModel
{
    Description = "Sample Template",
    Alias = "sample-template",
    Body = "This is my template {{var1}} and {{var2}}"
});

// Sending of templated messages
var message = new MessageCreateRequest
{
    To = "+254722000000",
    Template = new MessageTemplate
    {
        Alias = "sample-template",
        Model = new { var1 = "test", var2 = "test2" },
    }
};

// Send message, delivery shall be relayed via webhooks
var response = await client.Messages.CreateAsync(message);
response.EnsureSuccess(); // might throw an exception (FaluException)
```

> Templates should be created only once before use. Store the template ID or the Alias in your application and use either to reference the template.

## Payments

Below is a sample of how to request money from a customer via MPESA STK Push (a.k.a. Popup, Checkout, etc.).

```cs
FaluClient client; // omitted for brevity

var request = new PaymentRequest
{
    Amount = 1000,
    Currency = "kes",
    Mpesa = new PaymentRequestMpesaStkPush
    {
        Phone = "+254722000000",
        Reference = "<put-payment-reference-here>",
        Paybill = true, // false to tills (a.k.a Buygoods)
        Destination = "<put-short-code-here>", // required for specificity
    }
};

// Initiate the payment, completion shall be relayed via webhooks
var response = await client.Payments.CreateAsync(request);
response.EnsureSuccess(); // might throw an exception (FaluException)
```

> Your incoming account for MPESA must be configured in your [Workspace settings][workspace-settings] before you can initiate an outgoing payment to a customer.

## Transfers

With `FaluClient` you can send and receive money to and from customers or businesses via multiple payment providers. Below is a sample of how to send money to a customer via MPESA.

```cs
FaluClient client; // omitted for brevity

var request = new TransferRequest
{
    Amount = 1000,
    Currency = "kes",
    Purpose = "business", // can also be "salary"
    Mpesa = new TransferRequestMpesa
    {
        Customer = new TransferRequestMpesaToCustomer
        {
            Phone = "+254722000000",
            Source = "<put-short-code-here>", // required for specificity
        },
    }
};

// Initiate the transfer, completion shall be relayed via webhooks
var response = await client.Transfers.CreateAsync(request);
response.EnsureSuccess(); // might throw an exception (FaluException)
```

> Your outgoing account for MPESA must be configured in your [Workspace settings][workspace-settings] before you can initiate an outgoing payment to a customer.

## Development

For any requests, bug or comments, please [open an issue][issues] or [submit a pull request][pulls].

[api-docs]: https://docs.falu.io/api?lang=dotnet
[dotnet-core-cli-tools]: https://docs.microsoft.com/en-us/dotnet/core/tools/
[idempotency-keys]: https://docs.falu.io/api/idempotent_requests?lang=dotnet
[issues]: https://github.com/faluapp/falu-dotnet/issues/new
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
[pulls]: https://github.com/faluapp/falu-dotnet/pulls
[falu]: https://falu.io
[workspace-settings]: https://dashboard.falu.io/settings

### License

The Library is licensed under the [MIT](http://www.opensource.org/licenses/mit-license.php "Read more about the MIT license form") license. Refer to the [LICENSE](./LICENSE) file for more information.
