using Falu.Core;

namespace Falu.Customers;

/// <summary>Options for filtering and pagination of customers.</summary>
public record CustomersListOptions : BasicListOptions
{
    /// <summary>
    /// Filter options for <see cref="CustomerPatchModel.Email"/> property.
    /// </summary>
    public string? Email { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("email", Email);
    }
}
