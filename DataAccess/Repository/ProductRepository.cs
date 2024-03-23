using Microsoft.EntityFrameworkCore;
using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace DataAccess.Repository;

public class ProductRepository : IProductService<Product>
{

    private readonly GroceryStoreDbContext _context;

    public ProductRepository(GroceryStoreDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return _context.Products.Include(p => p.Category);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product prod)
    { 
        await _context.Products.AddAsync(prod);
       await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var productToDelete =await _context.Products.FindAsync(id);

         _context.Products.Remove(productToDelete);
         await _context.SaveChangesAsync();
    }

    public async Task<Product> UpdateAsync(Product prod, int id)
    {
        var prodToUpdate = await _context.Products.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == prod.Id);
       if (prodToUpdate is null)
       {
              return null;
       }

       prodToUpdate.Name = prod.Name;
       prodToUpdate.Description = prod.Description;
       prodToUpdate.Price = prod.Price;
       prodToUpdate.Category = prod.Category;
       prodToUpdate.ImageUrl = prod.ImageUrl;
       prodToUpdate.Stock = prod.Stock;
       prodToUpdate.Status = prod.Status;

        await _context.SaveChangesAsync();
       return prodToUpdate;
    }

    public async Task<Product> GetProductByNameAsync(string name)
    {
        return await _context.Products
            
            .FirstOrDefaultAsync(x => x.Name.ToLower()
                .Equals(name.ToLower()));
    }
   

}