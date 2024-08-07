﻿using Falu.Customers;
using Falu.Events;
using Falu.FileLinks;
using Falu.Files;
using Falu.IdentityVerificationReports;
using Falu.IdentityVerifications;
using Falu.MessageBatches;
using Falu.Messages;
using Falu.MessageStreams;
using Falu.MessageSuppressions;
using Falu.MessageTemplates;
using Falu.PaymentAuthorizations;
using Falu.PaymentRefunds;
using Falu.Payments;
using Falu.TemporaryKeys;
using Falu.TransferReversals;
using Falu.Transfers;
using Falu.Webhooks;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Falu;

/// <summary>
/// Official client for Falu API
/// </summary>
public class FaluClient<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TOptions> where TOptions : FaluClientOptions, new()
{
    /// <summary>
    /// Creates an instance of <see cref="FaluClient{TOptions}"/>
    /// </summary>
    /// <param name="backChannel"></param>
    /// <param name="optionsAccessor"></param>
    public FaluClient(HttpClient backChannel, IOptionsSnapshot<TOptions> optionsAccessor)
    {
        BackChannel = backChannel ?? throw new ArgumentNullException(nameof(backChannel));
        Options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

        // populate the User-Agent for 3rd party providers
        if (Options.Application is not null)
        {
            var userAgent = Options.Application.ToString();
            BackChannel.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        Customers = new CustomersServiceClient(BackChannel, Options);
        Events = new EventsServiceClient(BackChannel, Options);
        Files = new FilesServiceClient(BackChannel, Options);
        FileLinks = new FileLinksServiceClient(BackChannel, Options);
        IdentityVerifications = new IdentityVerificationsServiceClient(BackChannel, Options);
        IdentityVerificationReports = new IdentityVerificationReportsServiceClient(BackChannel, Options);
        Messages = new MessagesServiceClient(BackChannel, Options);
        MessageBatches = new MessageBatchesServiceClient(BackChannel, Options);
        MessageStreams = new MessageStreamsServiceClient(BackChannel, Options);
        MessageSuppressions = new MessageSuppressionsServiceClient(BackChannel, Options);
        MessageTemplates = new MessageTemplatesServiceClient(BackChannel, Options);
        MoneyBalances = new MoneyBalancesServiceClient(BackChannel, Options);
        Payments = new PaymentsServiceClient(BackChannel, Options);
        PaymentAuthorizations = new PaymentAuthorizationsServiceClient(BackChannel, Options);
        PaymentRefunds = new PaymentRefundsServiceClient(BackChannel, Options);
        Transfers = new TransfersServiceClient(BackChannel, Options);
        TransferReversals = new TransferReversalsServiceClient(BackChannel, Options);
        TemporaryKeys = new TemporaryKeysServiceClient(BackChannel, Options);
        Webhooks = new WebhooksServiceClient(BackChannel, Options);
    }

    ///
    protected HttpClient BackChannel { get; }

    ///
    protected TOptions Options { get; }

    #region Services

    ///
    public virtual CustomersServiceClient Customers { get; protected set; }

    ///
    public virtual EventsServiceClient Events { get; protected set; }

    ///
    public virtual FilesServiceClient Files { get; protected set; }

    ///
    public virtual FileLinksServiceClient FileLinks { get; protected set; }

    ///
    public virtual IdentityVerificationsServiceClient IdentityVerifications { get; protected set; }

    ///
    public virtual IdentityVerificationReportsServiceClient IdentityVerificationReports { get; protected set; }

    ///
    public virtual MessagesServiceClient Messages { get; protected set; }

    ///
    public virtual MessageBatchesServiceClient MessageBatches { get; protected set; }

    ///
    public virtual MessageStreamsServiceClient MessageStreams { get; protected set; }

    ///
    public virtual MessageSuppressionsServiceClient MessageSuppressions { get; protected set; }

    ///
    public virtual MessageTemplatesServiceClient MessageTemplates { get; protected set; }

    ///
    public virtual MoneyBalancesServiceClient MoneyBalances { get; protected set; }

    ///
    public virtual PaymentsServiceClient Payments { get; protected set; }

    ///
    public virtual PaymentAuthorizationsServiceClient PaymentAuthorizations { get; protected set; }

    ///
    public virtual PaymentRefundsServiceClient PaymentRefunds { get; protected set; }

    ///
    public virtual TransfersServiceClient Transfers { get; protected set; }

    ///
    public virtual TransferReversalsServiceClient TransferReversals { get; protected set; }

    ///
    public virtual TemporaryKeysServiceClient TemporaryKeys { get; protected set; }

    ///
    public virtual WebhooksServiceClient Webhooks { get; protected set; }

    #endregion
}

/// <summary>
/// Official client for Falu API
/// </summary>
/// <param name="backChannel"></param>
/// <param name="optionsAccessor"></param>
public class FaluClient(HttpClient backChannel, IOptionsSnapshot<FaluClientOptions> optionsAccessor) : FaluClient<FaluClientOptions>(backChannel, optionsAccessor) { }
