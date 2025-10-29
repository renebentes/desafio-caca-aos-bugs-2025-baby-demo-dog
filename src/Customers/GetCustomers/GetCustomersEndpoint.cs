using BugStore.Common.Primitives.Results;

namespace BugStore.Customers.GetCustomers;

internal static class GetCustomersEndpoint
{
    internal static IEndpointRouteBuilder MapGetCustomerEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapGet("/", HandleAsync)
            .WithName("Customers: Get")
            .WithSummary("Get all customers.")
            .WithDescription("Get all customers.")
            .Produces<IEnumerable<GetCustomersResponse>>();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        GetCustomersHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new GetCustomersRequest();
        Result<IEnumerable<GetCustomersResponse>> response = await handler.HandleAsync(
            request,
            cancellationToken);

        return TypedResults.Ok(response.Value);
    }
}
