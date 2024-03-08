using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTO;

namespace webbutveckling_labb2_Bjornanger.API.Extensions;

public class OrderRequest
{
    public int CustomerId { get; set; }
    public List<ProductOrderDto> ProductOrders { get; set; }
}