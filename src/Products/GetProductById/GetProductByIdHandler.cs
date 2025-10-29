using BugStore.Common.Primitives.Results;
using BugStore.Customers;
using BugStore.Customers.GetCustomerById;
using BugStore.Data;
using BugStore.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Products.GetProductById;

public sealed class GetProductByIdHandler(AppDbContext context)
    : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    public async Task<Result<GetProductByIdResponse>> HandleAsync(
        GetProductByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(
                c => c.Id == request.Id,
                cancellationToken: cancellationToken);

        if (product is null)
        {
            return Result<GetProductByIdResponse>.NotFound(
                CustomersErrors.NotFound(request.Id));
        }

        return Result<GetProductByIdResponse>.Success(product);
    }
}
