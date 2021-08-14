﻿using Falu.Evaluations;
using Falu.Events;
using Falu.Identity;
using Falu.Messages;
using Falu.MessageStreams;
using Falu.MessageTemplates;
using Falu.PaymentAuthorizations;
using Falu.PaymentRefunds;
using Falu.Payments;
using Falu.TransferReversals;
using Falu.Transfers;
using Falu.Webhooks;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Falu
{
    /// <summary>
    /// Official client for Falu API
    /// </summary>
    public class FaluClient
    {
        /// <summary>
        /// Creates an instance of <see cref="FaluClient"/>
        /// </summary>
        /// <param name="backChannel"></param>
        /// <param name="optionsAccessor"></param>
        public FaluClient(HttpClient backChannel, IOptions<FaluClientOptions> optionsAccessor)
        {
            if (backChannel is null) throw new ArgumentNullException(nameof(backChannel));

            var options = optionsAccessor?.Value;
            if (options is null) throw new ArgumentNullException(nameof(optionsAccessor));

            Evaluations = new EvaluationsService(backChannel, options);
            Identity = new IdentityService(backChannel, options);
            Messages = new MessagesService(backChannel, options);
            MoneyBalances = new MoneyBalancesService(backChannel, options);
            Payments = new PaymentsService(backChannel, options);
            PaymentAuthorizations = new PaymentAuthorizationsService(backChannel, options);
            PaymentRefunds = new PaymentRefundsService(backChannel, options);
            Transfers = new TransfersService(backChannel, options);
            TransferReversals = new TransferReversalsService(backChannel, options);
            MessageStreams = new MessageStreamsService(backChannel, options);
            MessageTemplates = new MessageTemplatesService(backChannel, options);
            Events = new EventsService(backChannel, options);
            Webhooks = new WebhooksService(backChannel, options);
        }

        ///
        public virtual EvaluationsService Evaluations { get; }

        ///
        public virtual EventsService Events { get; }

        ///
        public virtual IdentityService Identity { get; }

        ///
        public virtual MessagesService Messages { get; }

        ///
        public virtual MessageStreamsService MessageStreams { get; }

        ///
        public virtual MessageTemplatesService MessageTemplates { get; }

        ///
        public virtual MoneyBalancesService MoneyBalances { get; }

        ///
        public virtual PaymentsService Payments { get; }

        ///
        public virtual PaymentAuthorizationsService PaymentAuthorizations { get; }

        ///
        public virtual PaymentRefundsService PaymentRefunds { get; }

        ///
        public virtual TransfersService Transfers { get; }

        ///
        public virtual TransferReversalsService TransferReversals { get; }

        ///
        public virtual WebhooksService Webhooks { get; }
    }
}
