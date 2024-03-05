using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> ProductInCategory { get; set; } = new();



}