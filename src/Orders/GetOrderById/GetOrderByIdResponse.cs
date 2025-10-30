using BugStore.Models;

namespace BugStore.Orders.GetOrderById;

public sealed record GetOrderByIdResponse(
    Guid Id,
    string Customer,
    decimal Total,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IEnumerable<GetOrderByIdOrderLineResponse> Lines)
{
    public static implicit operator GetOrderByIdResponse(Order order)
        => new(
            order.Id,
            order.Customer.Name,
            order.GetTotalAmount(),
            order.CreatedAt,
            order.UpdatedAt,
            order.Lines
                .Select(
                    line => new GetOrderByIdOrderLineResponse(
                        line.Product.Title,
                        line.Quantity,
                        line.Total)));
}
