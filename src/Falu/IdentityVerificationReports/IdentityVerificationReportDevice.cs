namespace Falu.IdentityVerificationReports;

///
public class IdentityVerificationReportDevice : AbstractIdentityVerificationReportCheck
{
    /// <summary>
    /// User agent of the device from which the verification was done.
    /// </summary>
    /// <example>Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36</example>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Type of the client.
    /// </summary>
    /// <example>browser</example>
    public string? ClientType { get; set; }

    /// <summary>
    /// Type of the device.
    /// </summary>
    /// <example>smartphone</example>
    public string? Type { get; set; }

    /// <summary>
    /// Brand of the device.
    /// </summary>
    /// <example>Apple</example>
    public string? Brand { get; set; }

    /// <summary>
    /// Name or model of the device.
    /// </summary>
    /// <example>iPhone</example>
    public string? Model { get; set; }

    /// <summary>
    /// Operating system information.
    /// </summary>
    public IdentityVerificationReportDeviceOs? Os { get; set; }

    /// <summary>
    /// Browser information.
    /// Only populated when <c>client_type=browser</c>.
    /// </summary>
    public IdentityVerificationReportDeviceBrowser? Browser { get; set; }

    /// <summary>
    /// SDK information.
    /// Only populated when <c>client_type=sdk</c>.
    /// </summary>
    public IdentityVerificationReportDeviceSdk? Sdk { get; set; }
}

///
public class IdentityVerificationReportDeviceOs
{
    /// <summary>
    /// Name of the operating system.
    /// </summary>
    /// <example>iOS</example>
    public string? Name { get; set; }

    /// <summary>
    /// Type of operating system.
    /// </summary>
    /// <example>ios</example>
    public string? Type { get; set; }

    /// <summary>
    /// Version of the operating system.
    /// </summary>
    /// <example>16.0</example>
    public string? Version { get; set; }
}

///
public class IdentityVerificationReportDeviceBrowser
{
    /// <summary>
    /// Name of the browser.
    /// </summary>
    /// <example>safari</example>
    public string? Name { get; set; }

    /// <summary>
    /// Version of the browser.
    /// </summary>
    /// <example>16.0</example>
    public string? Version { get; set; }
}

///
public class IdentityVerificationReportDeviceSdk
{
    /// <summary>
    /// Name of the SDK.
    /// </summary>
    /// <example>falu-ios</example>
    public string? Name { get; set; }

    /// <summary>
    /// Version of the SDK.
    /// </summary>
    /// <example>2.2.11</example>
    public string? Version { get; set; }
}
