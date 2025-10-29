using BugStore.Common;
using BugStore.Customers.CreateCustomer;
using BugStore.Customers.DeleteCustomer;
using BugStore.Customers.GetCustomerById;
using BugStore.Customers.GetCustomers;
using BugStore.Customers.UpdateCustomer;
using BugStore.Data;
using BugStore.Products.CreateProduct;
using BugStore.Products.DeleteProduct;
using BugStore.Products.GetProductById;
using BugStore.Products.GetProducts;
using BugStore.Products.UpdateProduct;
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
            .AddTransient<DeleteCustomerHandler>()
            .AddTransient<GetCustomersHandler>()
            .AddTransient<GetCustomerByIdHandler>()
            .AddTransient<CreateProductHandler>()
            .AddTransient<UpdateProductHandler>()
            .AddTransient<DeleteProductHandler>()
            .AddTransient<GetProductsHandler>()
            .AddTransient<GetProductByIdHandler>();

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
            .AddProblemDetails();

        return services;
    }
}
