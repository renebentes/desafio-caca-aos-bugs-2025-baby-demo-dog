using BugStore.Messaging;
using BugStore.Models;

namespace BugStore.Products.CreateProduct;

public sealed record CreateProductRequest(
    string Title,
    string Description,
    string Slug,
    decimal Price)
    : IRequest<Guid>
{
    public static implicit operator Product(CreateProductRequest request)
        => new()
        {
            Title = request.Title,
            Description = request.Description,
            Slug = request.Slug.ToLowerInvariant().Replace(" ","-"),
            Price = request.Price
        };
}
