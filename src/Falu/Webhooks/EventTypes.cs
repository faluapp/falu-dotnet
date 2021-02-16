namespace Falu.Webhooks
{
    /// 
    public static class EventTypes
    {
        /// <summary>
        /// Occurs whenever an evaluation is created.
        /// </summary>
        public const string EvaluationCreated = "evaluation.created";

        /// <summary>
        /// Occurs whenever an evaluation fails.
        /// </summary>
        public const string EvaluationFailed = "evaluation.failed";

        /// <summary>
        /// Occurs whenever an evaluation is completed.
        /// </summary>
        public const string EvaluationCompleted = "evaluation.completed";


        /// <summary>
        /// Occurs whenever the balance has been updated.
        /// </summary>
        public const string BalanceUpdated = "balance.updated";

        /// <summary>
        /// Occurs whenever a payment is validated.
        /// This is only generated for incoming payments.
        /// </summary>
        public const string PaymentValidated = "payment.validated";

        /// <summary>
        /// Occurs whenever a payment is confirmed.
        /// This is only generated for incoming payments.
        /// </summary>
        public const string PaymentConfirmed = "payment.confirmed";

        /// <summary>
        /// Occurs whenever a payment is updated.
        /// </summary>
        public const string PaymentUpdated = "payment.updated";

        /// <summary>
        /// Occurs whenever a payment is cancelled.
        /// </summary>
        public const string PaymentCancelled = "payment.cancelled";

        /// <summary>
        /// Occurs whenever a payment reversal if updated.
        /// </summary>
        public const string ReversalUpdated = "payment.reversal.updated";

        /// <summary>
        /// Occurs whenever a push request completes.
        /// </summary>
        public const string PaymentPushCompleted = "payment.push_completed";

        /// <summary>
        /// Occurs whenever a push request times out.
        /// </summary>
        public const string PaymentPushTimedOut = "payment.push_timedout";


        /// <summary>
        /// Occurs whenever a message is sent.
        /// </summary>
        public const string MessageSent = "message.sent";

        /// <summary>
        /// Occurs whenever a message fails to send.
        /// </summary>
        public const string MessageFailed = "message.failed";

        /// <summary>
        /// Occurs whenever an SMS is delivered succesfully.
        /// NOTE: only called when a provider with delivery reports is enabled configured
        /// </summary>
        public const string MessageDelivered = "message.delivered";
    }
}
