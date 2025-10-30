namespace BugStore.Orders.GetOrderById;

public sealed record GetOrderByIdOrderLineResponse(
    string Product,
    int Quantity,
    decimal Total);
