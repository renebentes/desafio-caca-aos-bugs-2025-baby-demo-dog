using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;

namespace BugStore.Products.UpdateProduct;

public sealed class UpdateProductHandler(AppDbContext context)
    : IRequestHandler<UpdateProductRequest>
{
    public async Task<Result> HandleAsync(
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var customer = await context.Products.FindAsync([
            request.Id,
            cancellationToken
        ], cancellationToken: cancellationToken);

        if (customer is null)
        {
            return Result.NotFound(ProductsErrors.NotFound(request.Id));
        }

        context.Update(customer);
        await context.SaveChangesAsync(cancellationToken);
        return Result.NoContent();
    }
}
