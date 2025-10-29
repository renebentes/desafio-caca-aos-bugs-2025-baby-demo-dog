using BugStore.Common;
using BugStore.Common.Primitives.Results;
using BugStore.Customers.CreateCustomer;

namespace BugStore.Products.CreateProduct;

internal static class CreateProductEndpoint
{
    internal static IEndpointRouteBuilder MapCreateProductEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPost("/", HandleAsync)
            .WithName("Products: Create")
            .WithSummary("Create a new product.")
            .WithDescription("Create a new product.")
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        CreateProductRequest request,
        CreateProductHandler handler,
        CancellationToken cancellationToken = default)
    {
        Result<Guid> response = await handler.HandleAsync(request, cancellationToken);

        return response.IsSuccess
            ? TypedResults.Created($"/v1/products/{response.Value}", response.Value)
            : response.ToProblem();
    }
}
