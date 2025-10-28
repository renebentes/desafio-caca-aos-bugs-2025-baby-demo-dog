using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;

namespace BugStore.Customers.UpdateCustomer;

public sealed class UpdateCustomerHandler(AppDbContext context)
    : IRequestHandler<UpdateCustomerRequest, Result>
{
    public async Task<Result<Result>> HandleAsync(
        UpdateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        var customer = await context.Customers.FindAsync([
            request.Id,
            cancellationToken
        ], cancellationToken: cancellationToken);

        if (customer is null)
        {
            return Result.NotFound(CustomersErrors.NotFound(request.Id));
        }

        context.Update(customer);
        await context.SaveChangesAsync(cancellationToken);
        return Result.NoContent();
    }
}
