using BugStore.Common.Primitives.Results;
using BugStore.Customers;
using BugStore.Data;
using BugStore.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Orders.GetOrderById;

public sealed class GetOrderByIdHandler(AppDbContext context)
    : IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse>
{
    public async Task<Result<GetOrderByIdResponse>> HandleAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var order = await context.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(
                c => c.Id == request.Id,
                cancellationToken: cancellationToken);

        if (order is null)
        {
            return Result<GetOrderByIdResponse>.NotFound(
                CustomersErrors.NotFound(request.Id));
        }

        return Result<GetOrderByIdResponse>.Success(order);
    }
}
