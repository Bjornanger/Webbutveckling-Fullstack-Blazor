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
    public string ImageUrl { get; set; }  
    public int Stock { get; set; }

    public virtual ICollection<ProductsOrders> ProductOrders { get; set; }

}