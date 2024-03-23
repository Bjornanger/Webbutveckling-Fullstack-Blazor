using System.ComponentModel.DataAnnotations;

using webbutveckling_labb2_Bjornanger.Shared.DTOs.ProductDTOs;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;

public class CustomerDTO 
{

    public int Id { get; set; }
    [Required, StringLength(15, MinimumLength = 2)]
    public string FirstName { get; set; } = null!;
    [Required, StringLength(15, MinimumLength = 2)]
    public string LastName { get; set; } = null!;
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    [Required, StringLength(20, MinimumLength = 3)]
    public string Password { get; set; } = null!;

    [Required] public ContactInfoDTO ContactInfo { get; set; } = new(); 

    public virtual List<OrderDTO> Orders { get; set; } = new();
    public List<ProductDTO> Cart { get; set; } = new();
}