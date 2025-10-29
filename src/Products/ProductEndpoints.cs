using BugStore.Products.CreateProduct;
using BugStore.Products.DeleteProduct;
using BugStore.Products.GetProductById;
using BugStore.Products.GetProducts;
using BugStore.Products.UpdateProduct;

namespace BugStore.Products;

internal static class ProductEndpoints
{
    internal static WebApplication MapProductEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/products")
            .WithTags("Products")
            .MapCreateProductEndpoint()
            .MapUpdateProductEndpoint()
            .MapDeleteProductEndpoint()
            .MapGetProductsEndpoint()
            .MapGetProductByIdEndpoint();

        return app;
    }
}
