using BugStore.Models;

namespace BugStore.Customers.GetCustomers;

public sealed record GetCustomersResponse(
    Guid Id,
    string Name);
