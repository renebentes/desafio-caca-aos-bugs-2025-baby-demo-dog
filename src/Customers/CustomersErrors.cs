using BugStore.Common.Primitives.Results;

namespace BugStore.Customers;

public static class CustomersErrors
{
    public static readonly Error EmailNotUnique = new(
        "Customers.EmailNotUnique",
        "The provided email is not unique.");

    public static Error InvalidRequestId(Guid requestId)
        => new(
        "Customers.InvalidRequestId",
        $"The requestId= '{requestId}' was invalid.");

    public static Error NotFound(Guid customerId)
        => new(
        "Customers.NotFound",
        $"The customer with the requestId = '{customerId}' was not found.");
}
