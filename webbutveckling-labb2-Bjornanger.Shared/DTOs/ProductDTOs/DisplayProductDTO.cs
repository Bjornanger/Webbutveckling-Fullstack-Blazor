using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

public class DisplayProductDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]

    public double Price { get; set; }

    [Required]

    public CategoryDTO Category { get; set; }

    public bool Status { get; set; } = default; 
    [Url]
    public string ImageUrl { get; set; } // = "../image" - Kolla upp i LAbb2 Databaser.Lägg till en default bild
    public int Stock { get; set; }
}