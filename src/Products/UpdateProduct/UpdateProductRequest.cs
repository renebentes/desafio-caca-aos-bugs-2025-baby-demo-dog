using BugStore.Messaging;

namespace BugStore.Products.UpdateProduct;

public sealed record UpdateProductRequest(
    Guid Id,
    string Title,
    string Description,
    string Slug,
    Decimal Price)
    : IRequest;
