using BugStore.Common;

namespace BugStore.Customers.CreateCustomer;

internal static class CreateCustomerEndpoint
{
    internal static IEndpointRouteBuilder MapCreateCustomerEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPost("/", HandleAsync)
            .WithName("Customers: Create")
            .WithSummary("Create a new customer.")
            .WithDescription("Create a new customer.")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        CreateCustomerRequest request,
        CreateCustomerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(request, cancellationToken);
        return result.Map("/v1/customers");
    }
}
