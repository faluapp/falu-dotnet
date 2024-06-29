using System.Diagnostics.CodeAnalysis;

namespace Falu.Core;

///
public interface ISupportsUpdating<TResource, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TResourceUpdateOptions>
    where TResource : IHasId
    where TResourceUpdateOptions : class
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Unique identifier for the object.</param>
    /// <param name="options"></param>
    /// <param name="requestOptions">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResourceResponse<TResource>> UpdateAsync(string id,
                                                  TResourceUpdateOptions options,
                                                  RequestOptions? requestOptions = null,
                                                  CancellationToken cancellationToken = default);
}
