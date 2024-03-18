using webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace Webbutveckling.BlazorClient.Services;
//TODO: Om den inte används, kasta connection
public class UserService : ICustomerService<UserDTO>
{
    private readonly HttpClient _httpClient;


    public UserService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("BlazorAPI");
        
    }


    public Task<IEnumerable<UserDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(UserDTO entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCustomerPasswordAsync(int id, string newPassword)
    {
        throw new NotImplementedException();
    }
}