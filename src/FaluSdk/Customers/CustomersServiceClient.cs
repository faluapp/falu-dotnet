using Falu.Core;
using Tingle.Extensions.JsonPatch;

namespace Falu.Customers;

///
public class CustomersServiceClient : BaseServiceClient<Customer>,
                                      ISupportsListing<Customer, CustomersListOptions>,
                                      ISupportsRetrieving<Customer>,
                                      ISupportsCreation<Customer, CustomerCreateRequest>,
                                      ISupportsUpdating<Customer, CustomerPatchModel>
{
    ///
    public CustomersServiceClient(HttpClient backChannel, FaluClientOptions options) : base(backChannel, options) { }

    /// <inheritdoc/>
    protected override string BasePath => "/v1/customers";

    /// <summary>List customers.</summary>
    /// <inheritdoc/>
    public virtual Task<ResourceResponse<List<Customer>>> ListAsync(CustomersListOptions? options = null,
                                                                    RequestOptions? requestOptions = null,
                                                                    CancellationToken cancellationToken = default)
    {
        return ListResourcesAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>List customers recursively.</summary>
    /// <inheritdoc/>
    public virtual IAsyncEnumerable<Customer> ListRecursivelyAsync(CustomersListOptions? options = null,
                                                                   RequestOptions? requestOptions = null,
                                                                   CancellationToken cancellationToken = default)
    {
        return ListResourcesRecursivelyAsync(options, requestOptions, cancellationToken);
    }

    /// <summary>
    /// Retrieve a customer.
    /// </summary>
    /// <param name="id">Unique identifier for the customer</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Customer>> GetAsync(string id,
                                                             RequestOptions? options = null,
                                                             CancellationToken cancellationToken = default)
    {
        return GetResourceAsync(id, options, cancellationToken);
    }

    /// <summary>
    /// Create a customer.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Customer>> CreateAsync(CustomerCreateRequest request,
                                                                RequestOptions? options = null,
                                                                CancellationToken cancellationToken = default)
    {
        return CreateResourceAsync(request, options, cancellationToken);
    }

    /// <summary>
    /// Update a customer.
    /// </summary>
    /// <param name="id">Unique identifier for the customer</param>
    /// <param name="patch"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<Customer>> UpdateAsync(string id,
                                                                JsonPatchDocument<CustomerPatchModel> patch,
                                                                RequestOptions? options = null,
                                                                CancellationToken cancellationToken = default)
    {
        return UpdateResourceAsync(id, patch, options, cancellationToken);
    }

    /// <summary>
    /// Delete a customer.
    /// </summary>
    /// <param name="id">Unique identifier for the customer</param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<ResourceResponse<object>> DeleteAsync(string id,
                                                              RequestOptions? options = null,
                                                              CancellationToken cancellationToken = default)
    {
        return DeleteResourceAsync(id, options, cancellationToken);
    }
}
