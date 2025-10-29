using BugStore.Models;

namespace BugStore.Products.GetProducts;

public sealed record GetProductsResponse(
    Guid Id,
    string Title,
    decimal Price);
