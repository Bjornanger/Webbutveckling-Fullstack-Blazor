using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class User
{
    [Key]

    public int Id { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }


}