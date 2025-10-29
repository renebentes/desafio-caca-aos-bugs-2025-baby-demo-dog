using BugStore.Customers.GetCustomerById;
using BugStore.Messaging;

namespace BugStore.Products.GetProductById;

public sealed record GetProductByIdRequest(Guid Id)
    : IRequest<GetProductByIdResponse>;
