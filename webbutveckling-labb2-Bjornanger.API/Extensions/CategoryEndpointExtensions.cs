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

    private static async Task DeleteCategory(ICategoryService<Category> categoryRepo, int id)
    {

        var findCategoryToDelete = await categoryRepo.GetByIdAsync(id);

        if (findCategoryToDelete is null)
        {
            Results.NotFound($"The category with id {id} could not be found");
            return;
        }
        
        Results.Ok("Product deleted successfully");
        await categoryRepo.DeleteAsync(findCategoryToDelete.Id);


    }

    private static async Task AddCategory(ICategoryService<Category> repository, Category category)
    {

        var categoryToAdd =await repository.GetAllAsync();

        if (!categoryToAdd.ToList().Any(c => c.Name.ToLower().Equals(category.Name.ToLower())))
        {
            Results.BadRequest($"The category with name {category.Name} already exists");
            return;
        }

       
        Results.Ok("Category added");
        await repository.AddAsync(category);
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