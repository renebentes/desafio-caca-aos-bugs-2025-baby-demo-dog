using BugStore.Common;
using BugStore.Common.Primitives.Results;
using BugStore.Customers;
using BugStore.Data;
using BugStore.Messaging;
using BugStore.Models;
using BugStore.Products;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Orders.CreateOrder;

public sealed class CreateOrderHandler(
    AppDbContext context,
    IValidator<CreateOrderRequest> validator)
    : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
    public async Task<Result<CreateOrderResponse>> HandleAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        ValidationResult validationResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result<CreateOrderResponse>.Invalid(
                validationResult
                    .AsErrors()
            );
        }

        Customer? customer = await context.Customers
            .FirstOrDefaultAsync(
                c => c.Id == request.CustomerId,
                cancellationToken: cancellationToken);

        if (customer is null)
        {
            return Result<CreateOrderResponse>.NotFound(
                CustomersErrors.NotFound(request.CustomerId));
        }

        List<OrderLine> lines = [];
        foreach (var line in request.Lines)
        {
            Product? product = await context.Products
                .FirstOrDefaultAsync(
                    p => p.Id == line.ProductId,
                    cancellationToken);

            if (product is null)
            {
                return Result<CreateOrderResponse>.NotFound(
                    ProductsErrors.NotFound(line.ProductId));
            }

            lines.Add(new OrderLine
            {
                Product = product,
                Quantity = line.Quantity,
                Total = product.Price * line.Quantity
            });
        }

        var order = new Order(customer, lines);

        await context.Orders
            .AddAsync(order, cancellationToken);
        await context
            .SaveChangesAsync(cancellationToken);

        var response = new CreateOrderResponse(
            order.Id,
            order.GetTotalAmount());
        return Result<CreateOrderResponse>.Created(response);
    }
}
