using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Products.DeleteProduct;

public sealed class DeleteProductHandler(AppDbContext context)
    : IRequestHandler<DeleteProductRequest>
{
    public async Task<Result> HandleAsync(
        DeleteProductRequest request,
        CancellationToken cancellationToken = default)
    {
        Product? product = await context.Products
            .SingleOrDefaultAsync(
                p => p.Id == request.Id,
                cancellationToken);

        if (product is null)
        {
            return Result.NotFound(ProductsErrors.NotFound(request.Id));
        }

        context.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
        return Result.NoContent();
    }
}
