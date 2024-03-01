namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> DeleteAsync(int id);
    
}