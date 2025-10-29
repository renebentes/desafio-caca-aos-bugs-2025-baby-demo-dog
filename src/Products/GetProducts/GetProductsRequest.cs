using BugStore.Customers.GetCustomers;
using BugStore.Messaging;

namespace BugStore.Products.GetProducts;

public sealed record GetProductsRequest
    : IRequest<IEnumerable<GetProductsResponse>>;
