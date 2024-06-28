namespace Falu.Core;

///
public interface ISupportsCanceling<TResource> where TResource : IHasId, IHasRedaction
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Unique identifier for the object.</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> CancelAsync(string id,
                                                  RequestOptions? options = null,
                                                  CancellationToken cancellationToken = default);
}
