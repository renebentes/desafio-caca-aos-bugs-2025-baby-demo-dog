using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;

namespace BugStore.Customers.DeleteCustomer;

public sealed class DeleteCustomerHandler(AppDbContext context)
    : IRequestHandler<DeleteCustomerRequest>
{
    public async Task<Result> HandleAsync(
        DeleteCustomerRequest request,
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

        context.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);
        return Result.NoContent();
    }
}
