using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

public class ProductDTO
{
    public ProductDTO()
    {
        
    }
    public int Id { get; set; }
    [Required]
    [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [Range(1, 1000, ErrorMessage = "Price invalid (1-1000).")]
    public double Price { get; set; }

    public int Category { get; set; } = 0;


    [Range(typeof(bool), "true", "true", ErrorMessage = "Approval required.")]
    public bool Status { get; set; } = true;

    [Url] public string ImageUrl { get; set; } 
    public int Stock { get; set; }
}