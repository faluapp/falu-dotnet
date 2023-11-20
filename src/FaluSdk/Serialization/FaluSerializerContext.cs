using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Tingle.Extensions.JsonPatch;

namespace Falu.Serialization;

/// <summary>
/// Provides metadata about types used by Falu that is relevant to JSON serialization.
/// </summary>
[JsonSerializable(typeof(FaluError))]
[JsonSerializable(typeof(List<Tingle.Extensions.JsonPatch.Operations.Operation>))]

[JsonSerializable(typeof(List<Customers.Customer>))]
[JsonSerializable(typeof(Customers.CustomerCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<Customers.CustomerPatchModel>))]

[JsonSerializable(typeof(List<Files.File>))]
[JsonSerializable(typeof(Files.FileCreateRequest))]

[JsonSerializable(typeof(List<FileLinks.FileLink>))]
[JsonSerializable(typeof(FileLinks.FileLinkCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<FileLinks.FileLinkPatchModel>))]

[JsonSerializable(typeof(List<IdentityVerifications.IdentityVerification>))]
[JsonSerializable(typeof(IdentityVerifications.IdentityVerificationCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<IdentityVerifications.IdentityVerificationPatchModel>))]

[JsonSerializable(typeof(List<IdentityVerificationReports.IdentityVerificationReport>))]

[JsonSerializable(typeof(List<Messages.Message>))]
[JsonSerializable(typeof(Messages.MessageCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<Messages.MessagePatchModel>))]

[JsonSerializable(typeof(List<MessageBatches.MessageBatch>))]
[JsonSerializable(typeof(MessageBatches.MessageBatchCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<MessageBatches.MessageBatchPatchModel>))]
[JsonSerializable(typeof(MessageBatches.MessageBatchStatus))]

[JsonSerializable(typeof(List<MessageStreams.MessageStream>))]
[JsonSerializable(typeof(MessageStreams.MessageStreamCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<MessageStreams.MessageStreamPatchModel>))]
[JsonSerializable(typeof(MessageStreams.MessageStreamArchiveRequest))]
[JsonSerializable(typeof(MessageStreams.MessageStreamUnarchiveRequest))]

[JsonSerializable(typeof(List<MessageSuppressions.MessageSuppression>))]
[JsonSerializable(typeof(MessageSuppressions.MessageSuppressionCreateRequest))]

[JsonSerializable(typeof(List<MessageTemplates.MessageTemplate>))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<MessageTemplates.MessageTemplatePatchModel>))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateValidationRequest))]
[JsonSerializable(typeof(MessageTemplates.MessageTemplateValidationResponse))]

[JsonSerializable(typeof(Payments.MoneyBalances))]
[JsonSerializable(typeof(Payments.MoneyBalancesRefreshRequest))]
[JsonSerializable(typeof(Payments.MoneyBalancesRefreshResponse))]

[JsonSerializable(typeof(List<Payments.Payment>))]
[JsonSerializable(typeof(Payments.PaymentCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<Payments.PaymentPatchModel>))]

[JsonSerializable(typeof(List<PaymentAuthorizations.PaymentAuthorization>))]
[JsonSerializable(typeof(PaymentAuthorizations.PaymentAuthorizationApproveOptions))]
[JsonSerializable(typeof(PaymentAuthorizations.PaymentAuthorizationDeclineOptions))]
[JsonSerializable(typeof(JsonPatchDocument<PaymentAuthorizations.PaymentAuthorizationPatchModel>))]

[JsonSerializable(typeof(List<PaymentRefunds.PaymentRefund>))]
[JsonSerializable(typeof(PaymentRefunds.PaymentRefundCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<PaymentRefunds.PaymentRefundPatchModel>))]

[JsonSerializable(typeof(List<Transfers.Transfer>))]
[JsonSerializable(typeof(Transfers.TransferCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<Transfers.TransferPatchModel>))]

[JsonSerializable(typeof(List<TransferReversals.TransferReversal>))]
[JsonSerializable(typeof(TransferReversals.TransferReversalCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<TransferReversals.TransferReversalPatchModel>))]

[JsonSerializable(typeof(List<TemporaryKeys.TemporaryKey>))]
[JsonSerializable(typeof(TemporaryKeys.TemporaryKeyCreateRequest))]

[JsonSerializable(typeof(List<Webhooks.WebhookEndpoint>))]
[JsonSerializable(typeof(Webhooks.WebhookEndpointCreateRequest))]
[JsonSerializable(typeof(JsonPatchDocument<Webhooks.WebhookEndpointPatchModel>))]

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
    internal JsonTypeInfo<T> GetRequriedTypeInfo<T>()
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
