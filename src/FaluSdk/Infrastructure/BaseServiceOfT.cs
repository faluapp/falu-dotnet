using Falu.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tingle.Extensions.JsonPatch;

namespace Falu.Infrastructure
{
    ///
    public abstract class BaseService<TResource> : BaseService
    {
        /// <inheritdoc/>
        public BaseService(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <example>/v1/events</example>
        protected abstract string BasePath { get; }

        ///
        protected virtual Task<ResourceResponse<TResource>> GetResourceAsync(string id,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var uri = MakeResourcePath(id);
            return RequestAsync<TResource>(uri, HttpMethod.Get, null, options, cancellationToken);
        }


        ///
        protected virtual Task<ResourceResponse<TResource>> UpdateResourceAsync(string id,
                                                                                IJsonPatchDocument patch,
                                                                                RequestOptions? options = null,
                                                                                CancellationToken cancellationToken = default)
        {
            if (patch is null) throw new ArgumentNullException(nameof(patch));

            var uri = MakeResourcePath(id);
            return RequestAsync<TResource>(uri, HttpMethod.Patch, patch, options, cancellationToken);
        }


        ///
        protected virtual Task<ResourceResponse<object>> DeleteResourceAsync(string id,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
        {
            var uri = MakeResourcePath(id);
            return RequestAsync<object>(uri, HttpMethod.Delete, null, options, cancellationToken);
        }

        /// <summary>Generate path for a resource.</summary>
        /// <param name="id">Unique identifier of the resource.</param>
        /// <returns>The path for the resource.</returns>
        protected virtual string MakeResourcePath(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            return $"{BasePath}/{id}";
        }

    }
}
