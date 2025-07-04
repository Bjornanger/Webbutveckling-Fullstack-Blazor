﻿using System.Diagnostics.CodeAnalysis;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;
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
        group.MapPut("/{id}", UpdateProduct);
        group.MapPatch("/status/{id}", UpdateStatusOnProduct);
        group.MapDelete("/{id}", DeleteProduct);

        return app;
    }
   


private static async Task<IResult> UpdateStatusOnProduct(IProductService<Product> productRepo, int id)
    {
        var prod = await productRepo.GetByIdAsync(id);
        if (prod is null)
        {
            return Results.NotFound($"The product with id {id} could not be found");
        }

        prod.Status = !prod.Status;
        
        await productRepo.UpdateAsync(prod, id);

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

    private static async Task<IResult> UpdateProduct(IProductService<Product> productRepo, ICategoryService<Category> categoryRepo, ProductDTO productDTO, int id)
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

        Category switchedCategory = new Category();

        var newCategory = await categoryRepo.GetByIdAsync(productDTO.Category);


        if (newCategory is null)
        {
            return Results.NotFound($"Category with {newCategory.Id} not found");
        }


        switchedCategory = newCategory;

        Product product = new Product()
        {   Id = productDTO.Id,
            Name = productDTO.Name,
            Description = productDTO.Description,
            Price = productDTO.Price,
            ImageUrl = productDTO.ImageUrl,
            Category = switchedCategory,
            Stock = productDTO.Stock,
            Status = productDTO.Status
        };




        await productRepo.UpdateAsync(product, id);
        return Results.Ok();
    }


    private static async Task<IResult> AddProduct(IProductService<Product> repository,ICategoryService<Category> categoryRepo, ProductDTO productDto)
    {
        if (productDto is null)
        {
            return null;
        }
        var prodToAdd = await repository.GetAllAsync();
        

        if (prodToAdd is null)
        {
            return null;
        }

        Category switchedCategory = new Category();

        var newCategory = await categoryRepo.GetByIdAsync(productDto.Category);


        if (newCategory is null)
        {
            return Results.NotFound($"Category with {newCategory.Id} not found");
        }


        switchedCategory = newCategory;
        
        Product product = new Product()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            ImageUrl = productDto.ImageUrl,
            Category = switchedCategory,
            Stock = productDto.Stock,
            Status = productDto.Status
        };

        

        if (prodToAdd
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

    private static async Task<ProductDTO> GetProductByName(IProductService<Product> repository, string name)
    {

        var product = await repository.GetProductByNameAsync(name.ToLower());

        if (product is null)
        {
            Results.NotFound($"The product with name {name} could not be found");
            return null;
        }

        var prodToShowName = new ProductDTO
        {
            Name = product.Name,
           
        };


        Results.Ok();
        return prodToShowName;
    }

  
    private static async Task<ProductDTO> GetProductById(IProductService<Product> repository, int id)
    {

        var product = await repository.GetByIdAsync(id);

        if (product is null)
        {
            Results.NotFound($"The product with id {id} could not be found");
            return null;
        }

        var prodToFind = new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category.Id,
            Status = product.Status,
            ImageUrl = product.ImageUrl,
            Stock = product.Stock
        };
            
      

        Results.Ok();
        return prodToFind;
    }
    
    
    private static async Task<List<ProductDTO>> GetAllProducts(IProductService<Product> repository)
    {

        var products = await repository.GetAllAsync();
        
        if (products is null)
        {
            Results.NotFound("No products");
            return null;
        }

        var prodList = products.Select(p => new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category.Id,
            Status = p.Status,
            ImageUrl = p.ImageUrl,
            Stock = p.Stock
        }).ToList();

       
         Results.Ok($"{prodList}");
         return prodList;
       
    }
}