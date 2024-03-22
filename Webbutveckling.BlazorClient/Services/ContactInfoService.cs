using webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace Webbutveckling.BlazorClient.Services;

public class ContactInfoService : IContactInfoService<ContactInfoDTO>
{

    private readonly HttpClient _httpClient;

    public ContactInfoService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("BlazorAPI");
    }

    public async Task<ContactInfoDTO> UpdateCustomerInfo(int id, ContactInfoDTO entity)
    {
        await _httpClient.PutAsJsonAsync($"api/customer/{id}", entity);
        return await Task.FromResult(entity);
    }
}