using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class Customer : User
{
    [Required] 
    public string FirstName { get; set; }
    [Required]

    public string LastName { get; set; }
    [Required]

    public ContactInfo ContactInfo { get; set; }

    public virtual List<Order> Orders { get; set; } = new();
    public virtual List<Product> Cart { get; set; } = new();

    
   
}