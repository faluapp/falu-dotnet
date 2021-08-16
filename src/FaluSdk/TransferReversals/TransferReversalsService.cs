﻿using Falu.Core;
using Falu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.TransferReversals
{
    ///
    public class TransferReversalsService : BaseService
    {
        ///
        public TransferReversalsService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

        /// <summary>
        /// List transfer reversals.
        /// </summary>
        /// <param name="options">Options for filtering and pagination.</param>
        /// <param name="requestOptions">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<List<TransferReversal>>> ListAsync(TransferReversalsListOptions? options = null,
                                                                                      RequestOptions? requestOptions = null,
                                                                                      CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            var uri = new Uri(BaseAddress, $"/v1/transfer_reversals{query}");
            return await GetAsJsonAsync<List<TransferReversal>>(uri, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieve a transfer reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer reversal</param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<TransferReversal>> GetAsync(string id,
                                                                               RequestOptions? options = null,
                                                                               CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));

            var uri = new Uri(BaseAddress, $"/v1/transfer_reversals/{id}");
            return await GetAsJsonAsync<TransferReversal>(uri, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create transfer reversal.
        /// </summary>
        /// <param name="reversal"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<TransferReversal>> CreateAsync(TransferReversalRequest reversal,
                                                                                  RequestOptions? options = null,
                                                                                  CancellationToken cancellationToken = default)
        {
            if (reversal is null) throw new ArgumentNullException(nameof(reversal));

            var uri = new Uri(BaseAddress, "/v1/transfer_reversals");
            return await PostAsJsonAsync<TransferReversal>(uri, reversal, options, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Update a transfer reversal.
        /// </summary>
        /// <param name="id">Unique identifier for the transfer reversal</param>
        /// <param name="patch"></param>
        /// <param name="options">Options to use for the request.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<ResourceResponse<TransferReversal>> UpdateAsync(string id,
                                                                                  JsonPatchDocument<TransferReversalPatchModel> patch,
                                                                                  RequestOptions? options = null,
                                                                                  CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = new Uri(BaseAddress, $"/v1/transfer_reversals/{id}");
            return await PatchAsJsonAsync<TransferReversal>(uri, patch, options, cancellationToken).ConfigureAwait(false);
        }
    }
}