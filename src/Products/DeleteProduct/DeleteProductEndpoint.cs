using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Products.DeleteProduct;

internal static class DeleteProductEndpoint
{
    internal static IEndpointRouteBuilder MapDeleteProductEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapDelete("/{id:guid}", HandleAsync)
            .WithName("Products: Delete")
            .WithSummary("Remove an existing product.")
            .WithDescription("Remove an existing product.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Error>(StatusCodes.Status404NotFound);

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        DeleteProductHandler handler,
        CancellationToken cancellationToken = default)
    {
        DeleteProductRequest request = new(id);
        Result response = await handler.HandleAsync(request, cancellationToken);
        return response.IsSuccess
            ? TypedResults.NoContent()
            : response.ToProblem();
    }
}
