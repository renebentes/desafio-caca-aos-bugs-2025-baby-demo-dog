using FluentValidation;

namespace BugStore.Customers.CreateCustomer;

public sealed class CreateCustomerRequestValidator
    : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();
    }
}
