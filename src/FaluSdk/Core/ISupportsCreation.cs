namespace Falu.Core;

///
public interface ISupportsCreation<TResource, TResourceCreateRequest> where TResource : IHasId
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> CreateAsync(TResourceCreateRequest request,
                                                  RequestOptions? options = null,
                                                  CancellationToken cancellationToken = default);
}
