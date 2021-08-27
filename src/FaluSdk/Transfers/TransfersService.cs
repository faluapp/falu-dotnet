using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Transfers
{
    ///
    public class TransfersService : BaseService
    {
        ///
        public TransfersService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List transfers.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<Transfer>>> ListAsync(TransfersListOptions? options = null,
                                                                              RequestOptions? requestOptions = null,
                                                                              CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/transfers{query}");
            return await GetAsync<List<Transfer>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a transfer.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Transfer>> GetAsync(string id,
                                                                       RequestOptions? options = null,
                                                                       CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/transfers/{id}");
            return await GetAsync<Transfer>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a transfer.
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Transfer>> CreateAsync(TransferRequest transfer,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            if (transfer is null) throw new ArgumentNullException(nameof(transfer));

            var uri = new Uri(BaseAddress, "/v1/transfers");
            return await PostAsync<Transfer>(uri, transfer, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a transfer.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<Transfer>> UpdateAsync(string id,
                                                                          JsonPatchDocument<TransferPatchModel> patch,
                                                                          RequestOptions? options = null,
                                                                          CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/transfers/{id}");
            return await PatchAsync<Transfer>(uri, patch, options, cancellationToken).ConfigureAwait(false);
        }
    }
}
