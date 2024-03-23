using Microsoft.AspNetCore.Mvc;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace Webbutveckling.BlazorClient.Services;


public class ProductService : IProductService<ProductDTO>
{

    private readonly HttpClient _httpClient;


    public ProductService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("BlazorAPI");
    }




    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
    {
        var allProducts = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>("api/products");
        return await Task.FromResult(allProducts);

    }



    public async Task<ProductDTO> UpdateAsync(ProductDTO entity, int id)
    {
        await _httpClient.PutAsJsonAsync($"api/products/{id}", entity);
        return await Task.FromResult(entity);
    }
    
    public async Task<ProductDTO> GetProductByNameAsync(string name)
    {
        var productToShow = await _httpClient.GetFromJsonAsync<ProductDTO>($"api/products/name/{name}");
        return await Task.FromResult(productToShow);
    }
    public async Task<ProductDTO> GetByIdAsync(int id)
    {
        var productToShow = await _httpClient.GetFromJsonAsync<ProductDTO>($"api/products/{id}");
        return await Task.FromResult(productToShow);
    }

    public async Task AddAsync(ProductDTO product) 
    {
        var response = await _httpClient.PostAsJsonAsync("api/products", product);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to add {product}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/products/{id}");
    }


}