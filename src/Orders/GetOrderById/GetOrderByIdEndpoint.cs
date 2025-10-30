using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Orders.GetOrderById;

internal static class GetOrderByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetOrderByIdEndpoint(
        this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapGet("/{id:guid}", HandleAsync)
            .WithName("Orders: Get by Id")
            .WithSummary("Get order by identifier.")
            .WithDescription("Get order by identifier.")
            .Produces<GetOrderByIdResponse>();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        GetOrderByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new GetOrderByIdRequest(id);
        Result<GetOrderByIdResponse> response = await handler.HandleAsync(
            request,
            cancellationToken);

        return response.IsSuccess
            ? TypedResults.Ok(response.Value)
            : response.ToProblem();
    }
}
