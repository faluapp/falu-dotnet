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
using Falu.FileUploadLinks;

namespace Falu
{
    /// <summary>
    /// Official client for Falu API
    /// </summary>
    public class FaluClient<TOptions> where TOptions : FaluClientOptions
    {
        /// <summary>
        /// Creates an instance of <see cref="FaluClient{TOptions}"/>
        /// </summary>
        /// <param name="backChannel"></param>
        /// <param name="optionsAccessor"></param>
        public FaluClient(HttpClient backChannel, IOptions<TOptions> optionsAccessor)
        {
            BackChannel = backChannel ?? throw new ArgumentNullException(nameof(backChannel));
            Options = optionsAccessor?.Value ?? throw new ArgumentNullException(nameof(optionsAccessor));

            Evaluations = new EvaluationsService(BackChannel, Options);
            Events = new EventsService(BackChannel, Options);
            FileUploadLinks = new FileUploadLinksService(BackChannel, Options);
            Identity = new IdentityService(BackChannel, Options);
            Messages = new MessagesService(BackChannel, Options);
            MessageStreams = new MessageStreamsService(BackChannel, Options);
            MessageTemplates = new MessageTemplatesService(BackChannel, Options);
            MoneyBalances = new MoneyBalancesService(BackChannel, Options);
            Payments = new PaymentsService(BackChannel, Options);
            PaymentAuthorizations = new PaymentAuthorizationsService(BackChannel, Options);
            PaymentRefunds = new PaymentRefundsService(BackChannel, Options);
            Transfers = new TransfersService(BackChannel, Options);
            TransferReversals = new TransferReversalsService(BackChannel, Options);
            Webhooks = new WebhooksService(BackChannel, Options);
        }

        ///
        protected HttpClient BackChannel { get; }

        ///
        protected TOptions Options { get; }

        #region Services

        ///
        public virtual EvaluationsService Evaluations { get; protected set; }

        ///
        public virtual EventsService Events { get; protected set; }

        ///
        public virtual FileUploadLinksService FileUploadLinks { get; protected set; }

        ///
        public virtual IdentityService Identity { get; protected set; }

        ///
        public virtual MessagesService Messages { get; protected set; }

        ///
        public virtual MessageStreamsService MessageStreams { get; protected set; }

        ///
        public virtual MessageTemplatesService MessageTemplates { get; protected set; }

        ///
        public virtual MoneyBalancesService MoneyBalances { get; protected set; }

        ///
        public virtual PaymentsService Payments { get; protected set; }

        ///
        public virtual PaymentAuthorizationsService PaymentAuthorizations { get; protected set; }

        ///
        public virtual PaymentRefundsService PaymentRefunds { get; protected set; }

        ///
        public virtual TransfersService Transfers { get; protected set; }

        ///
        public virtual TransferReversalsService TransferReversals { get; protected set; }

        ///
        public virtual WebhooksService Webhooks { get; protected set; }

        #endregion
    }

    /// <summary>
    /// Official client for Falu API
    /// </summary>
    public class FaluClient : FaluClient<FaluClientOptions>
    {
        /// <summary>
        /// Creates an instance of <see cref="FaluClient"/>
        /// </summary>
        /// <param name="backChannel"></param>
        /// <param name="optionsAccessor"></param>
        public FaluClient(HttpClient backChannel, IOptions<FaluClientOptions> optionsAccessor) : base(backChannel, optionsAccessor) { }
    }
}
