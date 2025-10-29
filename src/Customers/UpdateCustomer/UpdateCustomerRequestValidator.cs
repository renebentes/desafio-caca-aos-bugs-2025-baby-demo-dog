using FluentValidation;

namespace BugStore.Customers.UpdateCustomer;

public sealed class UpdateCustomerRequestValidator
    : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();
    }
}
