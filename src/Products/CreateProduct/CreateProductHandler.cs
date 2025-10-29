using BugStore.Common;
using BugStore.Common.Primitives.Results;
using BugStore.Customers;
using BugStore.Customers.CreateCustomer;
using BugStore.Data;
using BugStore.Messaging;
using BugStore.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Products.CreateProduct;

public sealed class CreateProductHandler(
    AppDbContext context,
    IValidator<CreateProductRequest> validator)
    : IRequestHandler<CreateProductRequest, Guid>
{
    public async Task<Result<Guid>> HandleAsync(
        CreateProductRequest request,
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
            .Products
            .AsNoTracking()
            .AnyAsync(p => p.Slug == request.Slug, cancellationToken);

        if (emailExists)
        {
            return Result<Guid>.Conflict(CustomersErrors.EmailNotUnique);
        }

        Product product = request;

        _ = await context.Products.AddAsync(
            product,
            cancellationToken);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(product.Id);
    }
}
