using BugStore.Messaging;

namespace BugStore.Orders.CreateOrder;

public sealed record CreateOrderRequest(
    Guid CustomerId,
    IList<CreateOrderLineRequest> Lines)
    : IRequest<CreateOrderResponse>;
