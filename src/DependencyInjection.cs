using BugStore.Common;
using BugStore.Customers.CreateCustomer;
using BugStore.Customers.DeleteCustomer;
using BugStore.Customers.UpdateCustomer;
using BugStore.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BugStore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddTransient<CreateCustomerHandler>()
            .AddTransient<UpdateCustomerHandler>()
            .AddTransient<DeleteCustomerHandler>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                  throw new InvalidOperationException("Connection string was not found.");

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails();

        return services;
    }
}
