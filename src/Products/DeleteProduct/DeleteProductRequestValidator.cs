using BugStore.Customers.DeleteCustomer;
using FluentValidation;

namespace BugStore.Products.DeleteProduct;

public sealed class DeleteProductRequestValidator
    : AbstractValidator<DeleteCustomerRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
