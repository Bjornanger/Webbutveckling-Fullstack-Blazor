using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }

    public double TotalPrice { get; set; }

    public int CustomerId { get; set; }
    public virtual List<ProductDTO> ProductsInOrder { get; set; } = new List<ProductDTO>();

    
    public List<ProductOrderDto> ProductsAndAmount { get; set; }

    public bool Status { get; set; } // Sätta 0, 1 till status - mottagen / skickad
}