using System.Text.Json.Serialization;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public double TotalPrice { get; set; }

    public int CustomerId { get; set; }
    public virtual List<ProductDTO> ProductsInShoppingcart { get; set; } = new List<ProductDTO>();

    
    public List<ProductOrderDto> ProductOrders { get; set; }

  
}