namespace Falu.Webhooks;

/// 
public static class EventTypes
{
    #region Evaluation

    /// <summary>
    /// Occurs whenever an evaluation is created.
    /// </summary>
    public const string EvaluationCreated = "evaluation.created";

    /// <summary>
    /// Occurs whenever a user has successfully submitted their information,
    /// and scoring has started processing.
    /// </summary>
    public const string EvaluationProcessing = "evaluation.processing";

    /// <summary>
    /// Occurs whenever an evaluation is completed.
    /// </summary>
    public const string EvaluationCompleted = "evaluation.completed";

    /// <summary>
    /// Occurs whenever an evaluation transitions to require user input.
    /// </summary>
    public const string EvaluationInputRequired = "evaluation.input_required";

    /// <summary>
    /// Occurs whenever an evaluation has been redacted.
    /// </summary>
    public const string EvaluationRedacted = "evaluation.redacted";

    #endregion

    #region Files

    /// <summary>
    /// Occurs whenever a file is created.
    /// </summary>
    public const string FileCreated = "file.created";

    #endregion

    #region IdentityVerifications

    /// <summary>
    /// Occurs whenever an identity verification is created.
    /// </summary>
    public const string IdentityVerificationCreated = "identity_verification.created";

    /// <summary>
    /// Occurs whenever a user has successfully submitted their information,
    /// and verification checks have started processing.
    /// </summary>
    public const string IdentityVerificationProcessing = "identity_verification.processing";

    /// <summary>
    /// Occurs whenever processing of all the verification checks have completed
    /// and they’re all successfully verified.
    /// </summary>
    public const string IdentityVerificationVerified = "identity_verification.verified";

    /// <summary>
    /// Occurs whenever processing of all the verification checks have completed, and at least one of the checks failed.
    /// </summary>
    public const string IdentityVerificationInputRequired = "identity_verification.input_required";

    /// <summary>
    /// Occurs whenever an identity verification has been cancelled and future
    /// submission attempts have been disabled.
    /// </summary>
    public const string IdentityVerificationCancelled = "identity_verification.cancelled";

    /// <summary>
    /// Occurs whenever an identity verification has been redacted.
    /// </summary>
    public const string IdentityVerificationRedacted = "identity_verification.redacted";

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

    /// <summary>
    /// Occurs whenever the message has been cancelled and future
    /// update attempts have been disabled.
    /// </summary>
    public const string MessageCancelled = "message.cancelled";

    /// <summary>
    /// Occurs whenever a message has been redacted.
    /// </summary>
    public const string MessageRedacted = "message.redacted";

    #endregion

    #region Message Batches

    /// <summary>
    /// Occurs whenever a message batch is sent.
    /// </summary>
    public const string MessageBatchCreated = "message_batch.created";

    /// <summary>
    /// Occurs whenever a message batch is scheduled.
    /// </summary>
    public const string MessageBatchScheduled = "message_batch.scheduled";

    /// <summary>
    /// Occurs whenever the message batch has been cancelled and future
    /// update attempts have been disabled.
    /// </summary>
    public const string MessageBatchCancelled = "message_batch.cancelled";

    /// <summary>
    /// Occurs whenever a message batch has been redacted.
    /// </summary>
    public const string MessageBatchRedacted = "message_batch.redacted";

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

}
