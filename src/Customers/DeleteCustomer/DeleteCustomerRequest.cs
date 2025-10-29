using BugStore.Messaging;

namespace BugStore.Customers.DeleteCustomer;

public sealed record DeleteCustomerRequest(Guid Id)
    : IRequest;
