using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]

    public double Price { get; set; }

    [Required]

    public Category Category { get; set; }

    public bool Status { get; set; } = default; //Lägg in om det visar 0 skriv ut test - Out of Stock.
    [Url] 
    public string ImageUrl { get; set; } // = "../image" - Kolla upp i LAbb2 Databaser.Lägg till en default bild
    public int Stock { get; set; } 
}