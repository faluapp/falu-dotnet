namespace Falu.Webhooks
{
    /// <summary>
    /// The format to be used for a Webhook request.
    /// </summary>
    public enum WebhookFormat
    {
        /// <summary>
        /// Use the standard webhook format as explained in the docs.
        /// </summary>
        Basic = 0,

        /// <summary>
        /// Use the standardized events as governed by the
        /// <see href="https://cncf.io/">Cloud Native Computing Foundation (CNCF)</see>.
        /// For more information see the Cloud Events <see href="https://cloudevents.io/">website</see>
        /// and <see href="https://github.com/cloudevents/spec">official spec</see>.
        /// </summary>
        CloudEvents = 1
    }
}
