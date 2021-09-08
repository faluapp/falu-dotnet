using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Transfers
{
    ///
    public class TransfersServiceClient : BaseServiceClient<Transfer>, ISupportsListing<Transfer, TransfersListOptions>
    {
        ///
        public TransfersServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <inheritdoc/>
        protected override string BasePath => "/v1/transfers";

        /// <summary>List transfers.</summary>
        /// <inheritdoc/>
        public virtual Task<ResourceResponse<List<Transfer>>> ListAsync(TransfersListOptions? options = null,
                                                                        RequestOptions? requestOptions = null,
                                                                        CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>List transfers recursively.</summary>
        /// <inheritdoc/>
        public virtual IAsyncEnumerable<Transfer> ListRecursivelyAsync(TransfersListOptions? options = null,
                                                                       RequestOptions? requestOptions = null,
                                                                       CancellationToken cancellationToken = default)
        {
            return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
        }

        /// <summary>
        /// Retrieve a transfer.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Transfer>> GetAsync(string id,
                                                                 RequestOptions? options = null,
                                                                 CancellationToken cancellationToken = default)
        {
            return GetResourceAsync(id, options, cancellationToken);
        }

        /// <summary>
        /// Create a transfer.
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Transfer>> CreateAsync(TransferCreateRequest transfer,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
        {
            return CreateResourceAsync(transfer, options, cancellationToken);
        }

        /// <summary>
        /// Update a transfer.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<ResourceResponse<Transfer>> UpdateAsync(string id,
                                                                    JsonPatchDocument<TransferPatchModel> patch,
                                                                    RequestOptions? options = null,
                                                                    CancellationToken cancellationToken = default)
        {
            return UpdateResourceAsync(id, patch, options, cancellationToken);
        }
    }
}
