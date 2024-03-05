using webbutveckling_labb2_Bjornanger.Shared.Entities;

namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IOrderService<T> : IService<T> where T : class
{
    Task AddToProductOrders(ProductsOrders orders);
}