using BugStore.Messaging;

namespace BugStore.Products.DeleteProduct;

public sealed record DeleteProductRequest(Guid Id)
    : IRequest;
