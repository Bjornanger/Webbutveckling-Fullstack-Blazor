using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;

public class AdminDTO
{
    [Required]
    public string UserName { get; set; }
}