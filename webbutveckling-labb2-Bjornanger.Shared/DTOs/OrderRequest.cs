using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs;

public class OrderRequest
{
    public int CustomerId { get; set; }
    public List<ProductOrderDto> ProductOrders { get; set; }
}