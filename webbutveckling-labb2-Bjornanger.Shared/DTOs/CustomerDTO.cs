using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs;

public class CustomerDTO
{
    [Required]
    public string FirstName { get; set; }
    [Required]

    public string LastName { get; set; }
    [Required]

    public ContactInfoDTO ContactInfo { get; set; }

    public virtual List<OrderDTO> Orders { get; set; } = new();
    public virtual List<ProductDTO> Cart { get; set; } = new();
}