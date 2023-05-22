using System.Text.Json;
using System.Text.Json.Serialization;
using Tingle.Extensions.JsonPatch;

namespace Falu.Serialization;

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
[JsonSerializable(typeof(List<MessageStreams.MessageStreamArchiveRequest>))]
[JsonSerializable(typeof(List<MessageStreams.MessageStreamUnarchiveRequest>))]

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

[JsonSerializable(typeof(TemporaryKeys.TemporaryKey))]
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

[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<Customers.Customer>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<Files.File>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<FileLinks.FileLink>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<IdentityVerifications.IdentityVerification>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<IdentityVerificationReports.IdentityVerificationReport>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<Messages.Message>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<MessageBatches.MessageBatch>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<MessageStreams.MessageStream>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<MessageSuppressions.MessageSuppression>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<MessageTemplates.MessageTemplate>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<Payments.MoneyBalances>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<Payments.Payment>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<PaymentAuthorizations.PaymentAuthorization>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<PaymentRefunds.PaymentRefund>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<Transfers.Transfer>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<TransferReversals.TransferReversal>))]
[JsonSerializable(typeof(CloudNative.CloudEvents.CloudEventExtensions.CloudEventDataPayload<TemporaryKeys.TemporaryKey>))]
internal partial class FaluJsonSerializerContext : JsonSerializerContext
{
    private static JsonSerializerOptions DefaultSerializerOptions { get; } = new(JsonSerializerDefaults.Web)
    {
        AllowTrailingCommas = true,
        ReadCommentHandling = JsonCommentHandling.Skip,

        // Ignore default values to reduce the data sent after serialization
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,

        // Do not indent content to reduce data usage
        WriteIndented = false,

        // Use SnakeCase because it is what the server provides
        PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy(),
        DictionaryKeyPolicy = null,
    };

    static FaluJsonSerializerContext() => s_defaultContext = new FaluJsonSerializerContext(new JsonSerializerOptions(DefaultSerializerOptions));
}
