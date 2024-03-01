using DataAccess.Entities;
using DataAccess.Repository;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;


namespace BlazorLABB.Client.Extensions;

public static class CategoryEndpointExtensions
{

    
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories");

        group.MapGet("/", GetAllCategories);
        group.MapPost("/", AddCategory);
        group.MapDelete("/{id}", DeleteCategory);

        return app;
    }

    private static void DeleteCategory(ICategoryService<Category> repository, int id)
    {
        var categoryToDelete = repository.DeleteAsync(id);

        if (categoryToDelete is null)
        {
            Results.NotFound($"The category with id {id} could not be found");
            return;
        }

        repository.DeleteAsync(categoryToDelete.Id);
        Results.Ok("Product deleted successfully");

    }

    private static async void AddCategory(ICategoryService<Category> repository, Category category)
    {

        var categoryToAdd =await repository.GetAllAsync();

        if (!categoryToAdd.ToList().Any(c => c.Name.ToLower().Equals(category.Name.ToLower())))
        {
            Results.BadRequest($"The category with name {category.Name} already exists");
            return;
        }

        repository.AddAsync(category);
        Results.Ok("Category added");
    }

    private static async Task<List<Category>> GetAllCategories(ICategoryService<Category> repository)
    {
        var categories = await repository.GetAllAsync();

        if (categories.Count() <= 0)
        {
            Results.NotFound("No categories found");
        }

        Results.Ok("Category added");
        return categories.ToList();
    }

}