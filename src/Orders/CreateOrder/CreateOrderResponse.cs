namespace BugStore.Orders.CreateOrder;

public sealed record CreateOrderResponse(
    Guid OrderId,
    decimal TotalAmount);
