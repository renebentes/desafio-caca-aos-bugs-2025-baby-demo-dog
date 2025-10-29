using BugStore.Common;
using BugStore.Common.Primitives.Results;
using BugStore.Data;
using BugStore.Messaging;
using BugStore.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Customers.CreateCustomer;

public sealed class CreateCustomerHandler(
    AppDbContext context,
    IValidator<CreateCustomerRequest> validator)
    : IRequestHandler<CreateCustomerRequest, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        CreateCustomerRequest request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result<Guid>.Invalid(
                validationResult
                .AsErrors()
            );
        }

        bool emailExists = await context
            .Customers
            .AsNoTracking()
            .AnyAsync(c => c.Email == request.Email, cancellationToken);

        if (emailExists)
        {
            return Result<Guid>.Conflict(CustomersErrors.EmailNotUnique);
        }

        Customer customer = request;

        _ = await context.Customers.AddAsync(
            customer,
            cancellationToken);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(customer.Id);
    }
}
