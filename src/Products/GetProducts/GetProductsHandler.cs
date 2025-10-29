using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Products.GetProducts;

public sealed class GetProductsHandler(AppDbContext context)
    : IRequestHandler<GetProductsRequest, IEnumerable<GetProductsResponse>>
{
    public async Task<Result<IEnumerable<GetProductsResponse>>> HandleAsync(
        GetProductsRequest request,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<GetProductsResponse> products = await context.Products
            .AsNoTracking()
            .Select(p => new GetProductsResponse(
                p.Id,
                p.Title,
                p.Price))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result<IEnumerable<GetProductsResponse>>.Success(products);
    }
}
