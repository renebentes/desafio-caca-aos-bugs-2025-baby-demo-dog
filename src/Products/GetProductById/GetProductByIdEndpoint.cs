using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Products.GetProductById;

internal static class GetProductByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetProductByIdEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapGet("/{id:guid}", HandleAsync)
            .WithName("Products: Get by Id")
            .WithSummary("Get product by identifier.")
            .WithDescription("Get product by identifier.")
            .Produces<GetProductByIdResponse>();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        GetProductByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProductByIdRequest(id);
        Result<GetProductByIdResponse> response = await handler.HandleAsync(
            request,
            cancellationToken);

        return response.IsSuccess
            ? TypedResults.Ok(response.Value)
            : response.ToProblem();
    }
}
