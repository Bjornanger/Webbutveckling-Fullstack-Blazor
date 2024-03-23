using webbutveckling_labb2_Bjornanger.Shared.DTOs;
using webbutveckling_labb2_Bjornanger.Shared.Entities;
using webbutveckling_labb2_Bjornanger.Shared.Interfaces;

namespace Webbutveckling.BlazorClient.Services;

public class OrderService : IOrderService<OrderDTO>
{
    private readonly HttpClient _httpClient;

    public OrderService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("BlazorAPI");
    }


    public Task AddAsync(OrderDTO entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderDTO>> GetOrderFromCustomerAsync(int id)
    {
        var orderFromCustomer = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDTO>>($"api/customer/order/{id}");
        return await Task.FromResult(orderFromCustomer);

    }
    

    public async Task CreateCustomerOrderAsync(int id, OrderDTO entity)
    {
        await _httpClient.PostAsJsonAsync($"api/customer/{id}", entity);
    }
}