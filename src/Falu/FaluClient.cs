using Falu.Evaluations;
using Falu.Events;
using Falu.Identity;
using Falu.Messages;
using Falu.Payments;
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
            Payments = new PaymentsService(backChannel, options);
            PaymentReversals = new PaymentReversalsService(backChannel, options);
            Transfers = new TransfersService(backChannel, options);
            PaymentBalances = new PaymentBalancesService(backChannel, options);
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
        public virtual PaymentsService Payments { get; }

        ///
        public virtual PaymentReversalsService PaymentReversals { get; }

        ///
        public virtual TransfersService Transfers { get; }

        ///
        public virtual PaymentBalancesService PaymentBalances { get; }

        ///
        public virtual WebhooksService Webhooks { get; }
    }
}
