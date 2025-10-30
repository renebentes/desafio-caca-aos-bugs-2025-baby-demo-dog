using BugStore.Orders.CreateOrder;
using BugStore.Orders.GetOrderById;

namespace BugStore.Orders;

internal static class OrderEndpoints
{
    internal static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/orders")
            .WithTags("Orders")
            .MapCreateOrderEndpoint()
            .MapGetOrderByIdEndpoint();

        return app;
    }
}
