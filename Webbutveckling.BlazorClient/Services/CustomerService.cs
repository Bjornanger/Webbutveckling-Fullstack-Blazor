using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace Webbutveckling.BlazorClient.Services;

public class CustomerService : ICustomerService<CustomerDTO>
{


    private readonly HttpClient _httpClient;

    public CustomerService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("BlazorAPI");
    }

    public async Task<IEnumerable<CustomerDTO?>> GetAllAsync()
    {
       var allUsers = await _httpClient.GetFromJsonAsync<IEnumerable<CustomerDTO>>("api/users/customers");
       return await Task.FromResult(allUsers);
    }
   
    public async Task<CustomerDTO> GetByIdAsync(int id)
    {
        var customer = await _httpClient.GetFromJsonAsync<CustomerDTO>($"api/users/{id}");

       return await Task.FromResult(customer);
    }

    public async Task AddAsync(CustomerDTO entity)
    {
        await _httpClient.PostAsJsonAsync("api/users/customers", entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/users/{id}");
    }

    public async Task<CustomerDTO> GetUserByEmailAsync(string email)
    {
        var CustomerEmail =await _httpClient.GetFromJsonAsync<CustomerDTO>($"api/users/{email}");
        return await Task.FromResult<CustomerDTO>(CustomerEmail);
    }

    public async Task UpdateCustomerPasswordAsync(int id, string newPassword)
    {
        await _httpClient.PatchAsJsonAsync($"api/customer/password/{id}", newPassword);
        
    }

   
}