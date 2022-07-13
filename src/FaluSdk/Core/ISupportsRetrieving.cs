namespace Falu.Core;

///
public interface ISupportsRetrieving<TResource> where TResource : IHasId
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Unique identifier for the object.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> GetAsync(string id,
                                               RequestOptions? options = null,
                                               CancellationToken cancellationToken = default);
}
