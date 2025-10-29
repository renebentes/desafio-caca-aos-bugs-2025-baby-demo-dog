using BugStore.Customers.CreateCustomer;
using FluentValidation;

namespace BugStore.Products.CreateProduct;

public sealed class CreateProductRequestValidator
    : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
