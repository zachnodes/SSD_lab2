namespace SSD_Lab1.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    [Required]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "City")]
    [StringLength(50)]
    public string? City { get; set; }

    [Required]
    [Display(Name = "User Name")]
    public override string? UserName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public override string? Email { get; set; }

    [Phone]
    [Display(Name = "Phone Number")]
    public override string? PhoneNumber { get; set; }
}
