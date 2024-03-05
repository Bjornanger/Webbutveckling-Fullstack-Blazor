using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Admin : User
{
    [Required]
    public string UserName { get; set; }
}