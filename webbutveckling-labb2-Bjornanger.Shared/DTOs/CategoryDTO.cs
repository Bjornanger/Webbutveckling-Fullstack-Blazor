using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs;

public class CategoryDTO
{
    [Required]
    public string Name { get; set; }
    public List<ProductDTO.ProductDTO> ProductInCatagory { get; set; } = new();
}