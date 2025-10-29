using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Customers.GetCustomerById;

public sealed class GetCustomerByIdHandler(AppDbContext context)
    : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    public async Task<Result<GetCustomerByIdResponse>> HandleAsync(
        GetCustomerByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(
                c => c.Id == request.Id,
                cancellationToken: cancellationToken);

        if (customer is null)
        {
            if (customer is null)
            {
                return Result<GetCustomerByIdResponse>.NotFound(
                    CustomersErrors.NotFound(request.Id));
            }
        }

        return Result<GetCustomerByIdResponse>.Success(customer);
    }
}
