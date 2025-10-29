using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Customers.DeleteCustomer;

internal static class DeleteCustomerEndpoint
{
    internal static IEndpointRouteBuilder MapDeleteCustomerEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapDelete("/{id:guid}", HandleAsync)
            .WithName("Customers: Delete")
            .WithSummary("Remove an existing customer.")
            .WithDescription("Remove an existing customer.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        DeleteCustomerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteCustomerRequest(id);
        Result response = await handler.HandleAsync(request, cancellationToken);
        return response.IsSuccess
            ? TypedResults.NoContent()
            : response.ToProblem();
    }

}
