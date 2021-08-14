namespace Falu.Webhooks
{
    /// 
    public static class EventTypes
    {
        #region Evaluation

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

        #endregion

        #region Money Balances

        /// <summary>
        /// Occurs whenever a money balances are updated.
        /// </summary>
        public const string MoneyBalancesUpdated = "money_balances.updated";

        #endregion

        #region Payments

        /// <summary>
        /// Occurs whenever a payment is updated.
        /// </summary>
        public const string PaymentUpdated = "payment.updated";

        /// <summary>
        /// Occurs whenever a payment succeeds.
        /// </summary>
        public const string PaymentSucceeded = "payment.succeeded";

        /// <summary>
        /// Occurs whenever a payment fails.
        /// </summary>
        public const string PaymentFailed = "payment.failed";

        #endregion

        #region PaymentAuthorizations

        /// <summary>
        /// Occurs whenever a payment authorization is requested.
        /// </summary>
        public const string PaymentAuthorizationRequest = "payment.authorization.request";

        /// <summary>
        /// Occurs whenever a payment authorization is created.
        /// </summary>
        public const string PaymentAuthorizationCreated = "payment.authorization.created";

        /// <summary>
        /// Occurs whenever a payment authorization is updated.
        /// </summary>
        public const string PaymentAuthorizationUpdated = "payment.authorization.updated";

        #endregion

        #region PaymentRefunds

        /// <summary>
        /// Occurs whenever a payment refund is created.
        /// </summary>
        public const string PaymentRefundCreated = "payment.refund.created";

        /// <summary>
        /// Occurs whenever a payment refunc is updated.
        /// </summary>
        public const string PaymentRefundUpdated = "payment.refund.updated";

        /// <summary>
        /// Occurs whenever a payment refund succeeds.
        /// </summary>
        public const string PaymentRefundSucceeded = "payment.refund.succeeded";

        /// <summary>
        /// Occurs whenever a payment refund fails.
        /// </summary>
        public const string PaymentRefundFailed = "payment.refund.failed";

        #endregion

        #region Transfers

        /// <summary>
        /// Occurs whenever a transfer is created.
        /// </summary>
        public const string TransferCreated = "transfer.created";

        /// <summary>
        /// Occurs whenever a transfer is updated.
        /// </summary>
        public const string TransferUpdated = "transfer.updated";

        /// <summary>
        /// Occurs whenever a transfer succeeds.
        /// </summary>
        public const string TransferSucceeded = "transfer.succeeded";

        /// <summary>
        /// Occurs whenever a transfer fails.
        /// </summary>
        public const string TransferFailed = "transfer.failed";

        #endregion

        #region TransferReversals

        /// <summary>
        /// Occurs whenever a transfer reversal is created.
        /// </summary>
        public const string TransferReversalCreated = "transfer.reversal.created";

        /// <summary>
        /// Occurs whenever a transfer reversal is updated.
        /// </summary>
        public const string TransferReversalUpdated = "transfer.reversal.updated";

        /// <summary>
        /// Occurs whenever a transfer reversal succeeds.
        /// </summary>
        public const string TransferReversalSucceeded = "transfer.reversal.succeeded";

        /// <summary>
        /// Occurs whenever a transfer reversal fails.
        /// </summary>
        public const string TransferReversalFailed = "transfer.reversal.failed";

        #endregion

        #region Messages

        /// <summary>
        /// Occurs whenever a message is sent.
        /// </summary>
        public const string MessageSent = "message.sent";

        /// <summary>
        /// Occurs whenever a message fails to send.
        /// </summary>
        public const string MessageFailed = "message.failed";

        /// <summary>
        /// Occurs whenever an SMS is delivered successfully.
        /// NOTE: only called when a provider with delivery reports is enabled configured
        /// </summary>
        public const string MessageDelivered = "message.delivered";

        #endregion

        #region MessageTemplates

        /// <summary>
        /// Occurs whenever a message template is created.
        /// </summary>
        public const string MessageTemplateCreated = "message_template.created";

        /// <summary>
        /// Occurs whenever a message template is updated.
        /// </summary>
        public const string MessageTemplateUpdated = "message_template.updated";

        /// <summary>
        /// Occurs whenever a message template is deleted.
        /// </summary>
        public const string MessageTemplateDeleted = "message_template.deleted";

        #endregion

    }
}
