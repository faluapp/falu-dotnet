using Falu.Evaluations;
using Falu.Events;
using Falu.Identity;
using Falu.Messages;
using Falu.Payments;
using Falu.Templates;
using Falu.Webhooks;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace Falu
{
    /// <summary>
    /// Official client for Falu API
    /// </summary>
    public sealed class FaluClient
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
            PaymentsBalance = new PaymentsBalanceService(backChannel, options);
            PaymentsReversal = new PaymentsReversalsService(backChannel, options);
            Templates = new TemplatesService(backChannel, options);
            Events = new EventsService(backChannel, options);
            Webhooks = new WebhooksService(backChannel, options);
        }

        ///
        public EvaluationsService Evaluations { get; }

        ///
        public EventsService Events { get; }

        ///
        public IdentityService Identity { get; }

        ///
        public MessagesService Messages { get; }

        ///
        public PaymentsService Payments { get; }

        ///
        public PaymentsBalanceService PaymentsBalance { get; }

        ///
        public PaymentsReversalsService PaymentsReversal { get; }

        ///
        public TemplatesService Templates { get; }

        ///
        public WebhooksService Webhooks { get; }
    }
}
