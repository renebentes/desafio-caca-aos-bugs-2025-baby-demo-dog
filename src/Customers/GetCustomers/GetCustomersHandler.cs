using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Customers.GetCustomers;

public sealed class GetCustomersHandler(AppDbContext context)
    : IRequestHandler<GetCustomersRequest, IEnumerable<GetCustomersResponse>>
{
    public async Task<Result<IEnumerable<GetCustomersResponse>>> HandleAsync(
        GetCustomersRequest request,
        CancellationToken cancellationToken = default)
    {
        IEnumerable<GetCustomersResponse> customers = await context.Customers
            .AsNoTracking()
            .Select(c => new GetCustomersResponse(c.Id, c.Name))
            .ToListAsync(cancellationToken: cancellationToken);

        return Result<IEnumerable<GetCustomersResponse>>.Success(customers);
    }
}
