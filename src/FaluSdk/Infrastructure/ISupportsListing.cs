using Falu.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Falu.Infrastructure
{
    ///
    public interface ISupportsListing<TResource, TResourceListOptions>
        where TResource : IHasId
        where TResourceListOptions : BasicListOptions
    {
        ///
        Task<ResourceResponse<List<TResource>>> ListAsync(TResourceListOptions? options = null,
                                                          RequestOptions? requestOptions = null,
                                                          CancellationToken cancellationToken = default);
    }
}
