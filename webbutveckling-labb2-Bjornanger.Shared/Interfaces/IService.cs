namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task DeleteAsync(int id);
    
}