using System.Diagnostics.CodeAnalysis;
using Tingle.Extensions.JsonPatch;

namespace Falu.Core;

///
public interface ISupportsUpdating<TResource, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TResourcePatchModel>
    where TResource : IHasId
    where TResourcePatchModel : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Unique identifier for the object.</param>
    /// <param name="request"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> UpdateAsync(string id,
                                                  JsonPatchDocument<TResourcePatchModel> request,
                                                  RequestOptions? requestOptions = null,
                                                  CancellationToken cancellationToken = default);
}
