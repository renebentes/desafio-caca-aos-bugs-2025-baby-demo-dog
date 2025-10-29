using BugStore.Models;

namespace BugStore.Customers.GetCustomerById;

public sealed record GetCustomerByIdResponse(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    DateTime BirthDate)
{
    public static implicit operator GetCustomerByIdResponse(Customer customer)
        => new(
            customer.Id,
            customer.Name,
            customer.Email,
            customer.Phone,
            customer.BirthDate);
}
