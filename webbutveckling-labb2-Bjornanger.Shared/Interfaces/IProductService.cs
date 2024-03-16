using webbutveckling_labb2_Bjornanger.Shared.DTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IProductService <T>: IService<T> where T : class
{
  Task<T> UpdateAsync(T entity, int id);
    //TODO: fixa så att den inte kollapsar Databasen.

    Task<T> GetProductByNameAsync(string name);


}