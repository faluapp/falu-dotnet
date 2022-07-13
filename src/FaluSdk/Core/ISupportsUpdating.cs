using Tingle.Extensions.JsonPatch;

namespace Falu.Core;

///
public interface ISupportsUpdating<TResource, TResourcePatchModel>
    where TResource : IHasId, TResourcePatchModel
    where TResourcePatchModel : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Unique identifier for the object.</param>
    /// <param name="patch"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> UpdateAsync(string id,
                                                  JsonPatchDocument<TResourcePatchModel> patch,
                                                  RequestOptions? options = null,
                                                  CancellationToken cancellationToken = default);
}
