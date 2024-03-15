using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

public class ProductDTO
{
    public ProductDTO()
    {
        
    }

    [Required]
    [StringLength(16, ErrorMessage = "Identifier too long (16 character limit).")]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [Range(1, 1000, ErrorMessage = "Price invalid (1-1000).")]
    public double Price { get; set; }
    
    public int Category { get; set; }


    [Range(typeof(bool), "true", "true", ErrorMessage = "Approval required.")]
    public bool Status { get; set; } = true; //Lägg in om det visar 0 skriv ut test - Out of Stock.

    /*[Url]*/ public string ImageUrl { get; set; } = ".../webbutveckling-labb2-Bjornanger/webbutveckling-labb2-Bjornanger.API/ProductPictures/bjornanger/"; 
    public int Stock { get; set; }
}