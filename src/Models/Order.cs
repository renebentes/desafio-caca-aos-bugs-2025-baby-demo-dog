namespace BugStore.Models;

public sealed class Order
{
    private readonly List<OrderLine> _lines = [];

    public Order(
        Customer customer,
        List<OrderLine> lines)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        CreatedAt = DateTime.UtcNow;
        _lines = lines;
    }

    private Order()
    {
    }

    public DateTime CreatedAt { get; private set; }

    public Customer Customer { get; private init; }

    public Guid CustomerId { get; private set; }

    public Guid Id { get; private init; }

    public IReadOnlyList<OrderLine> Lines => _lines.AsReadOnly();

    public DateTime? UpdatedAt { get; private set; }

    public void AddLine(OrderLine line)
    {
        _lines.Add(line);
        UpdatedAt = DateTime.UtcNow;
    }

    public decimal GetTotalAmount()
        => Lines.Sum(line => line.Total);
}
