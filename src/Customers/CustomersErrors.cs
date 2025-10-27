using BugStore.Common.Primitives.Results;

namespace BugStore.Customers;

public static class CustomersErrors
{
    public static readonly Error EmailNotUnique = new(
        "Users.EmailNotUnique",
        "The provided email is not unique");
}
