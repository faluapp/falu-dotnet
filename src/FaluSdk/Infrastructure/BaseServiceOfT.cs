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
        public virtual async Task<ResourceResponse<List<TResource>>> ListResourcesAsync(BasicListOptions? options = null,
                                                                                        RequestOptions? requestOptions = null,
                                                                                        CancellationToken cancellationToken = default)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var uri = MakePathAndQuery(args);
            return await RequestAsync<List<TResource>>(uri, HttpMethod.Get, null, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        ///
        protected virtual Task<ResourceResponse<TResource>> CreateResourceAsync(object resource,
                                                                                RequestOptions? options = null,
                                                                                CancellationToken cancellationToken = default)
        {
            if (resource is null) throw new ArgumentNullException(nameof(resource));

            var uri = BasePath;
            return RequestAsync<TResource>(uri, HttpMethod.Post, resource, options, cancellationToken);
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

        /// <summary>Combine path and query.</summary>
        /// <param name="args">The keys and values to put in the query.</param>
        /// <returns>The path and query combined.</returns>
        protected virtual string MakePathAndQuery(Dictionary<string, string> args)
        {
            var query = QueryHelper.MakeQueryString(args);
            return MakePathAndQuery(query);
        }

        /// <summary>Combine path and query.</summary>
        /// <param name="query">The query to append.</param>
        /// <returns>The path and query combined.</returns>
        protected virtual string MakePathAndQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"'{nameof(query)}' cannot be null or whitespace.", nameof(query));
            }

            if (!query.StartsWith("?")) query = $"?{query}";
            return $"{BasePath}{query}";
        }
    }
}
