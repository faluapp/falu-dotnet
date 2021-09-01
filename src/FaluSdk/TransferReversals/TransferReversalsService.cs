using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.TransferReversals
{
    ///
    public class TransferReversalsService : BaseService<TransferReversal>, ISupportsListing<TransferReversal, TransferReversalsListOptions>
    {
        ///
        public TransferReversalsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/transfer_reversals";

        /// <summary>
        /// List transfer reversals.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<List<TransferReversal>>> ListAsync(TransferReversalsListOptions? options = null,
                                                                                RequestOptions? requestOptions = null,
                                                                                CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a transfer reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer reversal</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<TransferReversal>> GetAsync(string id,
                                                                         RequestOptions? options = null,
                                                                         CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create transfer reversal.
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<TransferReversal>> CreateAsync(TransferReversalCreateRequest reversal,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(reversal, options, cancellationToken);
        }

        /// <summary>
        /// Update a transfer reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer reversal</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<TransferReversal>> UpdateAsync(string id,
                                                                            JsonPatchDocument<TransferReversalPatchModel> patch,
                                                                            RequestOptions? options = null,
                                                                            CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
