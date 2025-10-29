using BugStore.Common;
using BugStore.Common.Primitives.Results;
using System;

namespace BugStore.Customers.GetCustomerById;

internal static class GetCustomerByIdEndpoint
{
    internal static IEndpointRouteBuilder MapGetCustomerByIdEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapGet("/{id:guid}", HandleAsync)
            .WithName("Customers: Get by Id")
            .WithSummary("Get customer by identifier.")
            .WithDescription("Get customer by identifier.")
            .Produces<GetCustomerByIdResponse>();

        return endpoint;
    }

    private static async Task<IResult> HandleAsync(
        Guid id,
        GetCustomerByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var request = new GetCustomerByIdRequest(id);
        Result<GetCustomerByIdResponse> response = await handler.HandleAsync(
            request,
            cancellationToken);

        return response.IsSuccess
            ? TypedResults.Ok(response.Value)
            : response.ToProblem();
    }
}
