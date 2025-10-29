using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Products.UpdateProduct;

public static class UpdateProductEndpoint
{
    internal static IEndpointRouteBuilder MapUpdateProductEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPut("/{id:guid}", HandleAsync)
            .WithName("Products: Update")
            .WithSummary("Update an existing product.")
            .WithDescription("Update an existing product.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        UpdateProductRequest request,
        Guid id,
        UpdateProductHandler handler,
        CancellationToken cancellationToken = default)
    {
        if (request.Id != id)
        {
            return TypedResults.ValidationProblem(
                ProductsErrors.InvalidRequestId(id)
                .ToValidationProblemError());
        }

        Result response = await handler.HandleAsync(request, cancellationToken);
        return response.IsSuccess
            ? TypedResults.NoContent()
            : response.ToProblem();
    }
}
