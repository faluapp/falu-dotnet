namespace Falu.Core;

///
public interface ISupportsCreation<TResource, TResourceCreateOptions> where TResource : IHasId
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> CreateAsync(TResourceCreateOptions options,
                                                  RequestOptions? requestOptions = null,
                                                  CancellationToken cancellationToken = default);
}
