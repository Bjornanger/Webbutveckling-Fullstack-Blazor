using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace webbutveckling_labb2_Bjornanger.API.Extensions;

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
        group.MapPatch("/status/{id}", UpdateStatusOnProduct);

        group.MapDelete("/{id}", DeleteProduct);

        return app;
    }

    private static async Task<IResult> UpdateStatusOnProduct(IProductService<Product> productRepo, int prodId)
    {
        var prod = await productRepo.GetByIdAsync(prodId);
        if (prod is null)
        {
            return Results.NotFound($"The product with id {prodId} could not be found");
        }

        prod.Status = !prod.Status;
        
        await productRepo.UpdateAsync(prod, prodId);

        return Results.Ok($"{prod.Status}");


    }

    private static async Task<IResult> DeleteProduct(IProductService<Product> repository, int id)
    {

        
        var product = await repository.GetByIdAsync(id);
        if (product is null)
        {
            return Results.NotFound($"The product with id {id} could not be found");
            
        }
        await repository.DeleteAsync(product.Id);
        return Results.Ok("Product deleted successfully");
    }

    private static async Task<IResult> UpdateProduct(IProductService<Product> productRepo, ICategoryService<Category> categoryRepo, Product product, int id)
    {
        var prod = await productRepo.GetByIdAsync(id);
        if (prod is null)
        {
            return Results.BadRequest($"The product with id {id} could not be found");
            
        }

        if (prod.Category.Id == null)
        {
            return Results.NotFound($"The product category {prod.Category.Id} is not found.");
        }

        await productRepo.UpdateAsync(product, id);
        return Results.Ok();
    }


    private static async Task<IResult> AddProduct(IProductService<Product> repository, Product product)
    {
        //TODO: Kontroll på att lagerstatus inte är 0 eller mindre

        var prodToAdd = await repository.GetAllAsync();

        if (prodToAdd is null)
        {
            return null;
        }


        if(prodToAdd
           .ToList().Any(p => p.Name.ToLower() == product.Name.ToLower()))
        {
            return Results.BadRequest($"The product with name {product.Name} already exists");
           
        }

        
        await repository.AddAsync(product);
         return Results.Ok($"{product.Name} added");

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

    private static async Task<Product> GetProductByName(IProductService<Product> repository, string name)
    {
        var productName = await repository.GetProductByNameAsync(name.ToLower());
        
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

        //fixa så att hela produkten skrivs ut i console/postman
       

        Results.Ok();
         return prodList.ToList();
    }
}