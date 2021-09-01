using Falu.Core;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        ///
        protected abstract string BasePath { get; }

        ///
        protected virtual Task<ResourceResponse<T>> GetResourceAsync<T>(string id,
                                                                        RequestOptions? options = null,
                                                                        CancellationToken cancellationToken = default)
        {
            var uri = MakeResourcePath(id);
            return RequestAsync<T>(uri, HttpMethod.Get, null, options, cancellationToken);
        }

        ///
        protected virtual Task<ResourceResponse<TResource>> GetResourceAsync(string id,
                                                                             RequestOptions? options = null,
                                                                             CancellationToken cancellationToken = default)
        {
            return GetResourceAsync<TResource>(id, options, cancellationToken);
        }

        ///
        public virtual async Task<ResourceResponse<List<T>>> ListResourcesAsync<T>(BasicListOptions? options = null,
                                                                                   RequestOptions? requestOptions = null,
                                                                                   CancellationToken cancellationToken = default)
        {
            var uri = MakePathWithQuery(null, options);
            return await RequestAsync<List<T>>(uri, HttpMethod.Get, null, requestOptions, cancellationToken).ConfigureAwait(false);
        }

        ///
        public virtual Task<ResourceResponse<List<TResource>>> ListResourcesAsync(BasicListOptions? options = null,
                                                                                  RequestOptions? requestOptions = null,
                                                                                  CancellationToken cancellationToken = default)
        {
            return ListResourcesAsync<TResource>(options, requestOptions, cancellationToken);
        }

        ///
        protected virtual Task<ResourceResponse<TResource>> CreateResourceAsync(object resource,
                                                                                RequestOptions? options = null,
                                                                                CancellationToken cancellationToken = default)
        {
            if (resource is null) throw new ArgumentNullException(nameof(resource));

            var uri = MakePath();
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

        #region Helpers

        /// <summary>Generate a path.</summary>
        /// <param name="subPath">The sub path to add at the end.</param>
        /// <returns>The generated path.</returns>
        protected virtual string MakePath(string? subPath = null) => $"{BasePath}{subPath}";

        /// <summary>Generate path for a resource.</summary>
        /// <param name="id">Unique identifier of the resource.</param>
        /// <returns>The path for the resource.</returns>
        protected virtual string MakeResourcePath(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
            }

            return $"{MakePath()}/{id}";
        }

        /// <summary>Combine path and query.</summary>
        /// <param name="subPath">The sub path to add before the query.</param>
        /// <param name="options">The options to generate keys and values for the query.</param>
        /// <returns>The path and query combined.</returns>
        protected virtual string MakePathWithQuery(string? subPath, BasicListOptions? options)
        {
            var args = new Dictionary<string, string>();
            options?.PopulateQueryValues(args);

            var query = QueryHelper.MakeQueryString(args);
            return MakePathWithQuery(subPath: subPath, query: query);
        }

        /// <summary>Combine path and query.</summary>
        /// <param name="subPath">The sub path to add before the query.</param>
        /// <param name="query">The query to append.</param>
        /// <returns>The path and query combined.</returns>
        protected virtual string MakePathWithQuery(string? subPath, string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentException($"'{nameof(query)}' cannot be null or whitespace.", nameof(query));
            }

            if (!query.StartsWith("?")) query = $"?{query}";
            return $"{MakePath(subPath)}{query}";
        }

        #endregion

    }
}
