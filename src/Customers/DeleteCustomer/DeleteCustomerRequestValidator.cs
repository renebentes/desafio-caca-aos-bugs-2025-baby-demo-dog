using FluentValidation;

namespace BugStore.Customers.DeleteCustomer;

public sealed class DeleteCustomerRequestValidator
    : AbstractValidator<DeleteCustomerRequest>
{
    public DeleteCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
