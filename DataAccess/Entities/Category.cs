using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> ProductInCatagory { get; set; } = new();



}