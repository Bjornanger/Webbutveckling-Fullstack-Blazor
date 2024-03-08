using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

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

    public bool Status { get; set; } = true;
    [Url] 
    public string ImageUrl { get; set; } // = "../image" - Kolla upp i Labb2 Databaser.Lägg till en default bild
    public int Stock { get; set; }

    public virtual ICollection<ProductsOrders> ProductOrders { get; set; }

}