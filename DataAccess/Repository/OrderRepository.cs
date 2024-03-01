using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace DataAccess.Repository;

public class OrderRepository : IOrderService<Order>
{
    private readonly GroceryStoreDbContext _context;

    public OrderRepository(GroceryStoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return _context.Orders;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task AddAsync(Order newOrder)
    {
        await _context.Orders.AddAsync(newOrder);
    }

    public async Task DeleteAsync(int id)
    {
       var orderToDelete = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(orderToDelete);
        await _context.SaveChangesAsync();
    }
}