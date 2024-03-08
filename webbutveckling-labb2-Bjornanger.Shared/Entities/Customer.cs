using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webbutveckling_labb2_Bjornanger.Shared.Entities;

public class Customer : User
{
    [Required] 
    public string FirstName { get; set; }
    [Required]

    public string LastName { get; set; }
    [Required]

    public ContactInfo ContactInfo { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}