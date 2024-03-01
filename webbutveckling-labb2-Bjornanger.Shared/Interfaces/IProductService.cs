namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IProductService
{
    Task<T> GetProductByIdAsync(int id);
    Task<IEnumerable<T>> GetProductsByCategoryIdAsync(int id);
    Task<IEnumerable<T>> GetProductsByNameAsync(string productName);

   }