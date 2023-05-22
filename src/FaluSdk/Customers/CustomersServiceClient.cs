using Falu.Core;
using Tingle.Extensions.JsonPatch;
using SC = Falu.Serialization.FaluSerializerContext;

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
    public virtual async Task<ResourceResponse<Customer>> CreateAsync(CustomerCreateRequest request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.CustomerCreateRequest, cancellationToken).ConfigureAwait(false);
        return await CreateResourceAsync(content, options, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Update a customer.
    /// </summary>
    /// <param name="id">Unique identifier for the customer</param>
    /// <param name="request"></param>
    /// <param name="options">Options to use for the request.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<ResourceResponse<Customer>> UpdateAsync(string id,
                                                                      JsonPatchDocument<CustomerPatchModel> request,
                                                                      RequestOptions? options = null,
                                                                      CancellationToken cancellationToken = default)
    {
        var content = await MakeJsonHttpContentAsync(request, SC.Default.JsonPatchDocumentCustomerPatchModel, cancellationToken).ConfigureAwait(false);
        return await UpdateResourceAsync(id, content, options, cancellationToken).ConfigureAwait(false);
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
        return DeleteResourceAsync(id, null, options, cancellationToken);
    }
}
