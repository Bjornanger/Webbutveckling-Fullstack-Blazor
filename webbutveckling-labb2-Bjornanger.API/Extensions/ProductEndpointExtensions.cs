using DataAccess.Entities;
using DataAccess.Repository;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;


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

    private static void DeleteProduct(IProductService<Product> repository, int id)
    {
        var product = repository.GetByIdAsync(id);
        if (product is null)
        {
            Results.NotFound($"The product with id {id} could not be found");
            return;
        }

        repository.DeleteAsync(product.Id);
        Results.Ok("Product deleted successfully");
    }

    private static async void UpdateProduct(IProductService<Product> repository, Product product, int id)
    {
       
        var prod = await repository.GetByIdAsync(id);
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

        await repository.UpdateAsync(prod);
        Results.Ok();
    }


    private static async void AddProduct(IProductService<Product> repository, Product product)
    {

        var prodToAdd = await repository.GetAllAsync();
        if(prodToAdd
           .ToList().Any(p => p.Name.ToLower() == product.Name.ToLower()))
        {
            Results.BadRequest($"The product with name {product.Name} already exists");
            return;
        }

        
        repository.AddAsync(product);
        Results.Ok();
    }

    private static async Task<List<Product>> GetProductsByCategory(IProductService<Product> repository,ICategoryService<Category> categoryRepo, string category)
    {
       
        var allProducts = await repository.GetAllAsync();

        var products = allProducts
            .ToList()
            .Where(p => p.Category.Name
                .ToLower() == category
                .ToLower())
            .ToList();

        var allCategories = await categoryRepo.GetAllAsync();

        
        if (!allCategories.Any(c => c.Name.ToLower().Equals(category.ToLower())))
        {
            Results.NotFound($"The category {category} could not be found");
            return null;

        }


        if (products is null || products.Count() <= 0)
        {
            Results.NotFound($"No products in category {category} could be found");
            return null;
        }

        Results.Ok();
        return products;
    }

    private static Task<Product> GetProductByName(IProductService<Product> repository, string name)
    {
        var productName = repository.GetProductByNameAsync(name.ToLower());
        
        if (productName is null)
        {
            Results.NotFound($"The product with name {name} could not be found");
            return null;
        }

        Results.Ok();
        return productName;
    }

    private static async Task<Product?> GetProductById(IProductService<Product> repository, int id)
    {
        var product = await repository.GetByIdAsync(id);
            
        if (product is null)
        {
            Results.NotFound($"The product with id {id} could not be found");
            return null;
        }

        Results.Ok();
        return product;
    }

    private static async Task<List<Product>> GetAllProducts(IProductService<Product> repository)
    {

        var products = await repository.GetAllAsync();

        var prodList = products.ToList();

        if (prodList.Count() <= 0)
        {
            Results.NotFound("No products in list");
            return null;
        }
        Results.Ok();
        return prodList;
    }
}