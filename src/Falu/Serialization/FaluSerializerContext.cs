using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Falu.Serialization;

/// <summary>
/// Provides metadata about types used by Falu that is relevant to JSON serialization.
/// </summary>
[JsonSerializable(typeof(FaluError))]

[JsonSerializable(typeof(List<Customers.Customer>))]
[JsonSerializable(typeof(Customers.CustomerCreateOptions))]
[JsonSerializable(typeof(Customers.CustomerUpdateOptions))]

[JsonSerializable(typeof(List<Files.File>))]
[JsonSerializable(typeof(Files.FileCreateOptions))]

[JsonSerializable(typeof(List<FileLinks.FileLink>))]
[JsonSerializable(typeof(FileLinks.FileLinkCreateOptions))]
[JsonSerializable(typeof(FileLinks.FileLinkUpdateOptions))]

[JsonSerializable(typeof(List<IdentityVerifications.IdentityVerification>))]
[JsonSerializable(typeof(IdentityVerifications.IdentityVerificationCreateOptions))]
[JsonSerializable(typeof(IdentityVerifications.IdentityVerificationUpdateOptions))]

[JsonSerializable(typeof(List<IdentityVerificationReports.IdentityVerificationReport>))]

[JsonSerializable(typeof(List<Messages.Message>))]
[JsonSerializable(typeof(Messages.MessageCreateOptions))]
[JsonSerializable(typeof(Messages.MessageUpdateOptions))]

[JsonSerializable(typeof(List<MessageBatches.MessageBatch>))]
[JsonSerializable(typeof(MessageBatches.MessageBatchCreateOptions))]
[JsonSerializable(typeof(MessageBatches.MessageBatchUpdateOptions))]
[JsonSerializable(typeof(MessageBatches.MessageBatchStatus))]

[JsonSerializable(typeof(List<MessageStreams.MessageStream>))]
[JsonSerializable(typeof(MessageStreams.MessageStreamCreateOptions))]
[JsonSerializable(typeof(MessageStreams.MessageStreamUpdateOptions))]
[JsonSerializable(typeof(MessageStreams.MessageStreamArchiveOptions))]
[JsonSerializable(typeof(MessageStreams.MessageStreamUnarchiveOptions))]

[JsonSerializable(typeof(List<MessageSuppressions.MessageSuppression>))]
[JsonSerializable(typeof(MessageSuppressions.MessageSuppressionCreateOptions))]

[JsonSerializable(typeof(List<MessageTemplates.MessageTemplate>))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateCreateOptions))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateUpdateOptions))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateValidationOptions))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateValidationResponse))]

[JsonSerializable(typeof(Payments.MoneyBalances))]
[JsonSerializable(typeof(Payments.MoneyBalancesRefreshOptions))]
[JsonSerializable(typeof(Payments.MoneyBalancesRefreshResponse))]

[JsonSerializable(typeof(List<Payments.Payment>))]
[JsonSerializable(typeof(Payments.PaymentCreateOptions))]
[JsonSerializable(typeof(Payments.PaymentUpdateOptions))]

[JsonSerializable(typeof(List<PaymentAuthorizations.PaymentAuthorization>))]
[JsonSerializable(typeof(PaymentAuthorizations.PaymentAuthorizationApproveOptions))]
[JsonSerializable(typeof(PaymentAuthorizations.PaymentAuthorizationDeclineOptions))]
[JsonSerializable(typeof(PaymentAuthorizations.PaymentAuthorizationUpdateOptions))]

[JsonSerializable(typeof(List<PaymentRefunds.PaymentRefund>))]
[JsonSerializable(typeof(PaymentRefunds.PaymentRefundCreateOptions))]
[JsonSerializable(typeof(PaymentRefunds.PaymentRefundUpdateOptions))]

[JsonSerializable(typeof(List<Transfers.Transfer>))]
[JsonSerializable(typeof(Transfers.TransferCreateOptions))]
[JsonSerializable(typeof(Transfers.TransferUpdateOptions))]

[JsonSerializable(typeof(List<TransferReversals.TransferReversal>))]
[JsonSerializable(typeof(TransferReversals.TransferReversalCreateOptions))]
[JsonSerializable(typeof(TransferReversals.TransferReversalUpdateOptions))]

[JsonSerializable(typeof(List<TemporaryKeys.TemporaryKey>))]
[JsonSerializable(typeof(TemporaryKeys.TemporaryKeyCreateOptions))]

[JsonSerializable(typeof(List<Webhooks.WebhookEndpoint>))]
[JsonSerializable(typeof(Webhooks.WebhookEndpointCreateOptions))]
[JsonSerializable(typeof(Webhooks.WebhookEndpointUpdateOptions))]

[JsonSerializable(typeof(List<Events.WebhookEvent>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<Customers.Customer>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<Files.File>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<FileLinks.FileLink>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<IdentityVerifications.IdentityVerification>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<IdentityVerificationReports.IdentityVerificationReport>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<Messages.Message>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<MessageBatches.MessageBatch>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<MessageStreams.MessageStream>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<MessageSuppressions.MessageSuppression>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<MessageTemplates.MessageTemplate>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<Payments.MoneyBalances>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<Payments.Payment>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<PaymentAuthorizations.PaymentAuthorization>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<PaymentRefunds.PaymentRefund>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<Transfers.Transfer>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<TransferReversals.TransferReversal>>))]
[JsonSerializable(typeof(List<Events.WebhookEvent<TemporaryKeys.TemporaryKey>>))]

[JsonSerializable(typeof(Events.CloudEventDataPayload<Customers.Customer>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<Files.File>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<FileLinks.FileLink>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<IdentityVerifications.IdentityVerification>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<IdentityVerificationReports.IdentityVerificationReport>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<Messages.Message>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<MessageBatches.MessageBatch>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<MessageStreams.MessageStream>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<MessageSuppressions.MessageSuppression>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<MessageTemplates.MessageTemplate>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<Payments.MoneyBalances>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<Payments.Payment>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<PaymentAuthorizations.PaymentAuthorization>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<PaymentRefunds.PaymentRefund>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<Transfers.Transfer>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<TransferReversals.TransferReversal>))]
[JsonSerializable(typeof(Events.CloudEventDataPayload<TemporaryKeys.TemporaryKey>))]

[JsonSourceGenerationOptions(
    AllowTrailingCommas = true,
    ReadCommentHandling = JsonCommentHandling.Skip,

    // Ignore default values to reduce the data sent after serialization
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,

    // Do not indent content to reduce data usage
    WriteIndented = false,

    // Use SnakeCase because it is what the server provides
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    DictionaryKeyPolicy = JsonKnownNamingPolicy.Unspecified
)]
public partial class FaluSerializerContext : JsonSerializerContext // This is exposed publicly to allow chaining via TypeInfoResolver in various external scenarios
{
    internal JsonTypeInfo<T>? GetTypeInfo<T>() => (JsonTypeInfo<T>?)GetTypeInfo(typeof(T));
    internal JsonTypeInfo<T> GetRequiredTypeInfo<T>()
    {
        var ti = GetTypeInfo<T>();
        if (ti is null)
        {
            throw new InvalidOperationException(
                $"'{typeof(T).FullName}' was not found in '{typeof(FaluSerializerContext).FullName}'." +
                $" You can either create an issue to have it added or use an overload that takes JsonTypeInfo<T> from your own JsonSerialzierContext.");
        }
        return ti;
    }
}
