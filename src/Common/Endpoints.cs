using BugStore.Customers;

namespace BugStore.Common;

public static class Endpoints
{
    internal static WebApplication MapEndpoints(this WebApplication app)
    {
        _ = app.MapGroup("");

        _ = app.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/",
                () => Results.Ok("Hello World."));

        _ = app.MapCustomerEndpoints();

        return app;
    }
}
