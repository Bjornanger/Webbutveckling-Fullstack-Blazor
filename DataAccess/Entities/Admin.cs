using System.ComponentModel.DataAnnotations;
using DataAccess.Entities;

namespace DataAccess.Entities;

public class Admin : User
{
    [Required]
    public string UserName { get; set; }
}