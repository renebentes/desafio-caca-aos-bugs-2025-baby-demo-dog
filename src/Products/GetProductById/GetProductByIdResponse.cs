using BugStore.Customers.GetCustomerById;
using BugStore.Models;

namespace BugStore.Products.GetProductById;

public sealed record GetProductByIdResponse(
    Guid Id,
    string Title,
    string Description,
    string Slug,
    decimal Price)
{
    public static implicit operator GetProductByIdResponse(Product product)
        => new(
            product.Id,
            product.Title,
            product.Description,
            product.Slug,
            product.Price);
}
