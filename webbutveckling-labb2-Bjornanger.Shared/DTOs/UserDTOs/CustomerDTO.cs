using System.ComponentModel.DataAnnotations;

using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;

public class CustomerDTO 
{

    public int Id { get; set; }
    [Required, StringLength(15, MinimumLength = 2)]
    public string FirstName { get; set; }
    [Required, StringLength(15, MinimumLength = 2)]
    public string LastName { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    [Required]
    public ContactInfoDTO ContactInfo { get; set; } //todo: cont- till int id

    public virtual List<OrderDTO> Orders { get; set; } = new();
    public virtual List<ProductDTO> Cart { get; set; } = new();
}