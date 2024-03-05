using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace webbutveckling_labb2_Bjornanger.API.Extensions;

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

    private static async Task<IResult> DeleteCategory(ICategoryService<Category> categoryRepo, int id)
    {

        var findCategoryToDelete = await categoryRepo.GetByIdAsync(id);

        if (findCategoryToDelete is null)
        {
            return Results.NotFound($"The category with id {id} could not be found");
            
        }


        await categoryRepo.DeleteAsync(findCategoryToDelete.Id);
        return Results.Ok("Category deleted successfully");
        


    }

    private static async Task<IResult> AddCategory(ICategoryService<Category> repository, Category category)
    {

        var categoryToAdd =await repository.GetAllAsync();

        if (categoryToAdd.ToList().Any(c => c.Name.ToLower().Equals(category.Name.ToLower())))
        {
            return Results.BadRequest($"The category with name {category.Name} already exists");
            
        }

        await repository.AddAsync(category);
        return Results.Ok("Category added");
        
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