namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface IContactInfoService<T> 
{
    Task<T> UpdateCustomerInfo(int id, T entity);
    
    
}