using FluentValidation;

namespace BugStore.Products.UpdateProduct;

public sealed class UpdateProductRequestValidator
    : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
