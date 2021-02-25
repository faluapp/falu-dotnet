namespace Falu.Core
{
    /// <summary>
    /// Options that can be included in all requests/operations.
    /// </summary>
    public class RequestOptions
    {
        /// <summary>
        /// The value to use for idempotent requests.
        /// This value is set in the <c>X-Idempotency-Key</c> request header.
        /// It can only be set for requests that are not idempotent by default.
        /// These are requests/operations that create new data or mutate existing data.
        /// </summary>
        public string IdempotencyKey { get; set; }

        /// <summary>
        /// The identifier of the workspace to target.
        /// This is only required when using user account bearer token, as the user may have access to one or more workspaces.
        /// For API key authentication, the key already identifies the workspace.
        /// This value is set in the <c>X-Falu-Workspace-Id</c> request header.
        /// </summary>
        public string Workspace { get; set; }

        /// <summary>
        /// Gets or sets value indicating if the request is targeted to the live or test environment.
        /// This is only required when using user account bearer token.
        /// For API key authentication, the key already identifies environment.
        /// This value is set in the <c>X-Live-Mode</c> request header.
        /// </summary>
        public bool Live { get; set; }
    }
}
