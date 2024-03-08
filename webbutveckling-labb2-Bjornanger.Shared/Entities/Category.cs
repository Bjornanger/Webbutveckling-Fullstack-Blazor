using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public List <Product> ProductInCategory { get; set; } = new();



}