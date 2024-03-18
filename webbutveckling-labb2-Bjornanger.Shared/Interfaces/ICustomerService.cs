using webbutveckling_labb2_Bjornanger.Shared.Entities;

namespace webbutveckling_labb2_Bjornanger.Shared.Interfaces;

public interface ICustomerService<T> : IService<T> where T : class
{
    Task UpdateCustomerPasswordAsync(int id, string newPassword);
}