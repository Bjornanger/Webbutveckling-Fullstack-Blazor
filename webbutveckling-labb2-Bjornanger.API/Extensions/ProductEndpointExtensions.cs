using DataAccess.Entities;
using DataAccess.Repository;


namespace BlazorLABB.Client.Extensions;

public static class ProductEndpointExtensions 
{

    public static IEndpointRouteBuilder MapProductEndpointExtensions(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/products");

        group.MapGet("/", GetAllProducts);
        group.MapGet("/{id:int}", GetProductById);
        group.MapGet("/name/{name}", GetProductByName);
        group.MapGet("/category/{category}", GetProductsByCategory);

        group.MapPost("/", AddProduct);
        group.MapPatch("/{id}", UpdateProduct);
        group.MapDelete("/{id}", DeleteProduct);

        return app;
    }

    private static void DeleteProduct(ProductRepository service, int id)
    {
        var product = service.Products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            Results.NotFound($"The product with id {id} could not be found");
            return;
        }

        service.Products.Remove(product);
        Results.Ok("Product deleted successfully");
    }

    private static void UpdateProduct(ProductRepository service, Product product, int id)
    {
        //TODO Se över denna metod
        var prod = service.Products.FirstOrDefault(p => p.Id == id);
        if (prod is null)
        {
            Results.BadRequest($"The product with id {id} could not be found");
            return;
        }

        prod.Name = product.Name;
        prod.Description = product.Description;
        prod.Price = product.Price;
        prod.Category = product.Category;
        prod.ImageUrl = product.ImageUrl;

        Results.Ok();
    }


    private static void AddProduct(ProductRepository service, Product product)
    {
        if (service.Products.Any(p => p.Name == product.Name))
        {
            Results.BadRequest($"The product with name {product.Name} already exists");
            return;
        }

        Results.Ok();
        service.Products.Add(product);
        //context.SaveChanges();
    }

    private static List<Product> GetProductsByCategory(ProductRepository service, string category)
    {
        

        var products = service.Products.Where(p => p.Category.Name == category).ToList();
        if (products is null || products.Count() <= 0)
        {
            Results.NotFound($"No products in category {category} could be found");
            return null;
        }

        Results.Ok();
        return products;
    }

    private static Product GetProductByName(ProductRepository service, string name)
    {
        var product = service.Products.FirstOrDefault(p => p.Name == name);

        if (product is null)
        {
            Results.NotFound($"The product with name {name} could not be found");
            return null;
        }

        Results.Ok();
        return product;
    }

    private static Product GetProductById(ProductRepository service, int id)
    {
        var product = service.Products.FirstOrDefault(p => p.Id == id);
        if (product is null)
        {
            Results.NotFound($"The product with id {id} could not be found");
            return null;
        }

        Results.Ok();
        return product;
    }

    private static List<Product> GetAllProducts(ProductRepository service)
    {

        var products = service.Products;


        if (products is null)
        {
            return null;
        }
        Results.Ok();
        return products.ToList();
    }
}