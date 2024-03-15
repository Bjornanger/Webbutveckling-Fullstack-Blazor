using System.ComponentModel.DataAnnotations;
using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
}