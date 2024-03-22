using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
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
        return _context.Orders.Include(o => o.ProductOrders);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.Include(o => o.ProductOrders)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Order newOrder)
    {
        await _context.Orders.AddAsync(newOrder);
        await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<Order>> GetOrderFromCustomerAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddToProductOrdersAsync(ProductsOrders orders)
    {
        throw new NotImplementedException();
    }

    public async Task CreateCustomerOrderAsync(int id, Order entity)
    {
       var customerToAddOrderTo = await _context.Customers.FindAsync(id);
       customerToAddOrderTo.Orders.Add(entity); 
       await _context.SaveChangesAsync();
    }

    

    public async Task DeleteAsync(int id)
    {
        var orderToDelete = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(orderToDelete);
        await _context.SaveChangesAsync();
    }



    public async Task AddToProductOrders(ProductsOrders orders)
    {
        await _context.ProductOrders.AddAsync(orders);
        await _context.SaveChangesAsync();
    }
}