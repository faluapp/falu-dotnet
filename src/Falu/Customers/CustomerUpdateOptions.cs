using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.Customers;

/// <summary>
/// A model representing details that can be changed about a customer
/// </summary>
public class CustomerUpdateOptions : IHasOptionalDescription, IHasOptionalMetadata
{
    private Optional<string?>? name;
    private Optional<string?>? phone;
    private Optional<string?>? email;
    private Optional<string?>? description;
    private Optional<PhysicalAddress?>? address;
    private Optional<Dictionary<string, string>?>? metadata;

    /// <summary>
    /// The customer’s primary address.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<PhysicalAddress?>))]
    public Optional<PhysicalAddress?>? Address { get => address; set => address = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Description { get => description; set => description = new(value); }

    /// <summary>
    /// The customer’s email address.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Email { get => email; set => email = new(value); }

    /// <summary>
    /// The customer’s full name or business name.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Name { get => name; set => name = new(name); }

    /// <summary>
    /// The customer’s phone number.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Phone { get => phone; set => phone = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }
}
