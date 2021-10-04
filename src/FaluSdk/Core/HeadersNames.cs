namespace Falu.Core
{
    internal static class HeadersNames
    {
        // Request
        public const string XFaluVersion = "X-Falu-Version";
        public const string XIdempotencyKey = "X-Idempotency-Key";
        public const string XWorkspaceId = "X-Workspace-Id";
        public const string XLiveMode = "X-Live-Mode";

        // Response
        public const string XRequestId = "X-Request-Id";
        public const string XTraceId = "X-Trace-Id";
        public const string XContinuationToken = "X-Continuation-Token";
        public const string XCachedResponse = "X-Cached-Response";
        public const string XShouldRetry = "X-Should-Retry";
        public const string RetryAfter = "Retry-After";

        // Webhooks
        public const string XFaluSignature = "X-Falu-Signature";
    }
}
