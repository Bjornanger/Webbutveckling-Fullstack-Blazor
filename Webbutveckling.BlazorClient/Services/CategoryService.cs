using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace Webbutveckling.BlazorClient.Services;

public class CategoryService :ICategoryService<CategoryDTO>
{
    private readonly HttpClient _httpClient;

    public CategoryService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("BlazorAPI");
    }
    


    public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
    {
        var allCategories = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>("api/categories");
        return await Task.FromResult(allCategories);
    }

    public async Task<CategoryDTO> GetByIdAsync(int id)
    {
        var categoryToFind = await _httpClient.GetFromJsonAsync<CategoryDTO>($"api/categories/{id}");
        return await Task.FromResult(categoryToFind);
    }

    public async Task AddAsync(CategoryDTO entity)
    {
       await _httpClient.PostAsJsonAsync("api/categories", entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/categories/{id}");
       
    }
}