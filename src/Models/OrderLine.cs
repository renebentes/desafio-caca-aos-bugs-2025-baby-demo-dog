namespace BugStore.Models;

public sealed class OrderLine
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }

    public required int Quantity { get; set; }
    public required decimal Total { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
