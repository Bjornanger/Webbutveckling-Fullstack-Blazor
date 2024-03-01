using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

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