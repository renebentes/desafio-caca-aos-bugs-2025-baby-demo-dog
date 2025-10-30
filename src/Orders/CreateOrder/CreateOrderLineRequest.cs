namespace BugStore.Orders.CreateOrder;

public sealed record CreateOrderLineRequest(
    Guid ProductId,
    int Quantity);
