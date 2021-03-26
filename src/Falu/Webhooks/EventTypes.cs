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

        #region Payments

        /// <summary>
        /// Occurs whenever the balance has been updated.
        /// </summary>
        public const string BalanceUpdated = "balance.updated";

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

        /// <summary>
        /// Occurs whenever a payment reversal is created.
        /// </summary>
        public const string PaymentReversalCreated = "payment.reversal.created";

        /// <summary>
        /// Occurs whenever a payment reversal is updated.
        /// </summary>
        public const string PaymentReversalUpdated = "payment.reversal.updated";

        /// <summary>
        /// Occurs whenever a payment reversal succeeds.
        /// </summary>
        public const string PaymentReversalSucceeded = "payment.reversal.succeeded";

        /// <summary>
        /// Occurs whenever a payment reversal fails.
        /// </summary>
        public const string PaymentReversalFailed = "payment.reversal.failed";

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
        /// Occurs whenever an SMS is delivered succesfully.
        /// NOTE: only called when a provider with delivery reports is enabled configured
        /// </summary>
        public const string MessageDelivered = "message.delivered";

        #endregion

    }
}
