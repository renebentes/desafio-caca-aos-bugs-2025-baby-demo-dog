using BugStore.Messaging;

namespace BugStore.Customers.GetCustomers;

public sealed record GetCustomersRequest
    : IRequest<IEnumerable<GetCustomersResponse>>;
