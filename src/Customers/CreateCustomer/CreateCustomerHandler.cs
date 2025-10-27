using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;
using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Customers.CreateCustomer;

public sealed class CreateCustomerHandler(AppDbContext context)
    : IRequestHandler<CreateCustomerRequest, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {

        bool emailExists = await context
            .Customers
            .AsNoTracking()
            .AnyAsync(c => c.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            return Result.NotFound(CustomersErrors.EmailNotUnique);
        }

        Customer customer = request;

        _ = await context.Customers.AddAsync(
            customer,
            cancellationToken);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(customer.Id);
    }
}