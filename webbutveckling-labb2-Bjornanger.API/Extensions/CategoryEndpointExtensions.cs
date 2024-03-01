using DataAccess.Entities;
using DataAccess.Repository;


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

    private static void DeleteCategory(CategoryRepository service, int id)
    {
        var categoryToDelete = service.Categories.FirstOrDefault(c => c.Id == id);

        if (categoryToDelete is null)
        {
            Results.NotFound();
            return;
        }

        service.Categories.Remove(categoryToDelete);

    }

    private static void AddCategory(CategoryRepository service, Category category)
    {
        if (service.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
        {
            Results.BadRequest($"The category with name {category.Name} already exists");
            return;
        }

        service.Categories.Add(category);
    }

    private static List<Category> GetAllCategories(CategoryRepository service)
    {
        var categories = service.Categories;

        if (categories.Count == 0)
        {
            Results.NotFound();
        }

        return categories;
    }
}