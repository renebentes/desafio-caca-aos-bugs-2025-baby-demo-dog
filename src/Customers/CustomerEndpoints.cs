using BugStore.Customers.CreateCustomer;
using BugStore.Customers.DeleteCustomer;
using BugStore.Customers.UpdateCustomer;

namespace BugStore.Customers;

internal static class CustomerEndpoints
{
    internal static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/customers")
            .WithTags("Customers")
            .MapCreateCustomerEndpoint()
            .MapUpdateCustomerEndpoint()
            .MapDeleteCustomerEndpoint();

        return app;
    }
}
