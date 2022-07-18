﻿using Falu.Core;

namespace Falu.Webhooks;

/// <summary>Options for filtering and pagination of webhook endpoints.</summary>
public record WebhookEndpointsListOptions : BasicListOptions
{
    /// <summary>Filter options for <see cref="WebhookEndpointPatchModel.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status);
    }
}
