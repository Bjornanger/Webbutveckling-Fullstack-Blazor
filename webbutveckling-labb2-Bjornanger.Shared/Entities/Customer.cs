using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Customer : User
{
    [Required] 
    public string FirstName { get; set; }
    [Required]

    public string LastName { get; set; }
    [Required]

    public ContactInfo ContactInfo { get; set; }

    public virtual List<int> Orders { get; set; } = new();
    public virtual List<Product> Cart { get; set; } = new();

    
   
}