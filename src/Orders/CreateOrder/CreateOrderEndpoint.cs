using BugStore.Common;
using BugStore.Common.Primitives.Results;

namespace BugStore.Orders.CreateOrder;

internal static class CreateOrderEndpoint
{
    internal static IEndpointRouteBuilder MapCreateOrderEndpoint(
        this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPost("/", HandleAsync)
            .WithName("Orders: Create")
            .WithSummary("Create a new order.")
            .WithDescription("Create a new order.")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesValidationProblem();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        CreateOrderRequest request,
        CreateOrderHandler handler,
        CancellationToken cancellationToken = default)
    {
        Result<CreateOrderResponse> response = await handler.HandleAsync(
            request,
            cancellationToken);

        return response.IsSuccess
            ? TypedResults.Created($"/v1/orders/{response.Value}", response.Value)
            : response.ToProblem();
    }
}
