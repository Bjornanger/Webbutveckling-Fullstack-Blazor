using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.Entities;

namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IOrderService<T> 
{
    Task AddAsync(T entity);
    
    Task<IEnumerable<T>> GetOrderFromCustomerAsync(int id);
    
    Task CreateCustomerOrderAsync(int id,T entity);


}