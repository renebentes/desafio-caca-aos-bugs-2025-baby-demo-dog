using FluentValidation;

namespace BugStore.Orders.CreateOrder;

public class CreateOrderRequestValidator
    : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(o => o.CustomerId)
            .NotEmpty();

        RuleFor(o => o.Lines)
            .NotEmpty();

        RuleForEach(o => o.Lines)
            .ChildRules(line =>
            {
                line.RuleFor(l => l.ProductId)
                    .NotEmpty();

                line.RuleFor(l => l.Quantity)
                    .GreaterThan(0);
            });
    }
}
