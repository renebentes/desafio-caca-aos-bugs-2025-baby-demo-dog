using BugStore.Messaging;

namespace BugStore.Customers.GetCustomerById;

public sealed record GetCustomerByIdRequest(Guid Id)
    : IRequest<GetCustomerByIdResponse>;
