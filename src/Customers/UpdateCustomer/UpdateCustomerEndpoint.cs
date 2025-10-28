using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Customers.UpdateCustomer;

public static class UpdateCustomerEndpoint
{
    internal static IEndpointRouteBuilder MapUpdateCustomerEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPut("/{id:guid}", HandleAsync)
            .WithName("Customers: Update")
            .WithSummary("Update an existing customer.")
            .WithDescription("Update an existing customer.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        UpdateCustomerRequest request,
        Guid id,
        UpdateCustomerHandler handler,
        CancellationToken cancellationToken = default)
    {
        if (request.Id != id)
        {
            return Results.BadRequest(CustomersErrors.InvalidRequestId);
        }

        var result = await handler.HandleAsync(request, cancellationToken);
        return result.Map();
    }
}
