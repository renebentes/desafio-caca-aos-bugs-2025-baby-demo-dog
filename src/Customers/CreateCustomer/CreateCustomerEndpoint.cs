using BugStore.Common;
using BugStore.Common.Primitives.Results;

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
        Result<Guid> response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? TypedResults.Created($"/v1/customers/{response.Value}", response.Value)
            : response.ToProblem();
    }
}
