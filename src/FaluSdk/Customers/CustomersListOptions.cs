using Falu.Core;

namespace Falu.Customers;

/// <summary>Options for filtering and pagination of customers.</summary>
public record CustomersListOptions : BasicListOptions
{
    /// <summary>
    /// Filter options for <see cref="CustomerUpdateOptions.Email"/> property.
    /// </summary>
    public string? Email { get; set; }

    /// <inheritdoc/>
    protected internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("email", Email);
    }
}
