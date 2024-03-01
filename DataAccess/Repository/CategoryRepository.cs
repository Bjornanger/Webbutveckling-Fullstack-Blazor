using DataAccess.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace DataAccess.Repository;

public class CategoryRepository : ICategoryService<Category>
{
    private readonly GroceryStoreDbContext _context;

    public CategoryRepository(GroceryStoreDbContext context)
    {
        _context = context;
    }


    //public List<Category> Categories { get; set; } = new()
    //{
    //    new()
    //    {
    //        Id = 1,
    //        Name = "Everything"
    //    }
    //};
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
       return _context.Categories;
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category newCategory)
    {
        await _context.Categories.AddAsync(newCategory);
        await _context.SaveChangesAsync();
        
    }

    public async Task DeleteAsync(int id)
    {
        var categoryToDelete = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(categoryToDelete);
        await _context.SaveChangesAsync();
    }
}