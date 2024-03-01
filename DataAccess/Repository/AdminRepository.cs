using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace DataAccess.Repository;

public class AdminRepository : IAdminService<Admin>
{

    private readonly GroceryStoreDbContext _context;

    public AdminRepository(GroceryStoreDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Admin>> GetAllAsync()
    {
        return _context.Admins;
    }

    public async Task<Admin> GetByIdAsync(int id)
    {
       return _context.Admins.Find(id);
    }

    public async Task AddAsync(Admin newAdmin)
    {
        await _context.Admins.AddAsync(newAdmin);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var adminToRemove =await _context.Admins.FindAsync(id);
        _context.Admins.Remove(adminToRemove);
       await _context.SaveChangesAsync();
    }
}