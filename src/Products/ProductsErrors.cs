using BugStore.Common.Primitives.Results;

namespace BugStore.Products;

public static class ProductsErrors
{
    public static readonly Error EmailNotUnique = new(
        "Products.SlugNotUnique",
        "The provided slug is not unique.");

    public static Error InvalidRequestId(Guid requestId)
        => new(
        "Products.InvalidRequestId",
        $"The requestId= '{requestId}' was invalid.");

    public static Error NotFound(Guid productId)
        => new(
        "Products.NotFound",
        $"The product with the requestId = '{productId}' was not found.");
}
