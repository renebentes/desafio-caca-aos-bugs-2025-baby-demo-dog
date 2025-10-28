using BugStore.Common.Primitives.Results;
using BugStore.Messaging;

namespace BugStore.Customers.UpdateCustomer;

public sealed record UpdateCustomerRequest(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    DateTime BirthDate)
    : IRequest<Result>;
