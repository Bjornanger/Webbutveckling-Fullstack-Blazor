using System.ComponentModel.DataAnnotations;

namespace webbutveckling_labb2_Bjornanger.Shared.DTOs.UserDTOs;

public class ContactInfoDTO
{
    public int Id { get; set; }
    [Required]
    [Phone]
    public string Phone { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string ZipCode { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    [Required]

    public string Country { get; set; } = null!;

}