using BugStore.Messaging;
using BugStore.Models;

namespace BugStore.Customers.CreateCustomer;

public sealed record CreateCustomerRequest(
    string Name,
    string Email,
    string Phone,
    DateTime BirthDate)
    : IRequest<Guid>
{
    public static implicit operator Customer(CreateCustomerRequest request)
        => new()
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };
}