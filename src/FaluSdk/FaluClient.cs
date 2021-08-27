using Falu.Evaluations;
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
            BackChannel = backChannel ?? throw new ArgumentNullException(nameof(backChannel));
            Options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

            Evaluations = new EvaluationsService(BackChannel, Options);
            Identity = new IdentityService(BackChannel, Options);
            Messages = new MessagesService(BackChannel, Options);
            MoneyBalances = new MoneyBalancesService(BackChannel, Options);
            Payments = new PaymentsService(BackChannel, Options);
            PaymentAuthorizations = new PaymentAuthorizationsService(BackChannel, Options);
            PaymentRefunds = new PaymentRefundsService(BackChannel, Options);
            Transfers = new TransfersService(BackChannel, Options);
            TransferReversals = new TransferReversalsService(BackChannel, Options);
            MessageStreams = new MessageStreamsService(BackChannel, Options);
            MessageTemplates = new MessageTemplatesService(BackChannel, Options);
            Events = new EventsService(BackChannel, Options);
            Webhooks = new WebhooksService(BackChannel, Options);
        }

        ///
        protected HttpClient BackChannel { get; }

        ///
        protected FaluClientOptions Options { get; }

        #region Services

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

        #endregion
    }
}
