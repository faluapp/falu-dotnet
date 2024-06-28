namespace Falu.Core;

///
public interface ISupportsRedaction<TResource> where TResource : IHasId, IHasRedaction
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Unique identifier for the object.</param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> RedactAsync(string id,
                                                  RequestOptions? requestOptions = null,
                                                  CancellationToken cancellationToken = default);
}
