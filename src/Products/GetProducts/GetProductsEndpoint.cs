using BugStore.Common.Primitives.Results;

namespace BugStore.Products.GetProducts;

internal static class GetProductsEndpoint
{
    internal static IEndpointRouteBuilder MapGetProductsEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapGet("/", HandleAsync)
            .WithName("Products: Get")
            .WithSummary("Get all products.")
            .WithDescription("Get all products.")
            .Produces<IEnumerable<GetProductsResponse>>();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        GetProductsHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProductsRequest();
        Result<IEnumerable<GetProductsResponse>> response = await handler.HandleAsync(
            request,
            cancellationToken);

        return TypedResults.Ok(response.Value);
    }
}
