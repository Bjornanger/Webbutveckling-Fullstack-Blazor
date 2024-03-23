namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

public class ProductOrderDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Amount { get; set; }
}