namespace Falu.Core;

///
public interface ISupportsListing<TResource, TResourceListOptions>
    where TResource : IHasId
    where TResourceListOptions : BasicListOptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options">Options for filtering and pagination.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<List<TResource>>> ListAsync(TResourceListOptions? options = null,
                                                      RequestOptions? requestOptions = null,
                                                      CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options">Options for filtering and pagination.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IAsyncEnumerable<TResource> ListRecursivelyAsync(TResourceListOptions? options = null,
                                                     RequestOptions? requestOptions = null,
                                                     CancellationToken cancellationToken = default);
}
