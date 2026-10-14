namespace SSD_Lab1.Models;

using System.ComponentModel.DataAnnotations;

public class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Company Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, 500)]
    [Display(Name = "Years in Business")]
    public int YearsInBusiness { get; set; }

    [Required]
    [Url]
    [StringLength(200)]
    [Display(Name = "Website")]
    public string Website { get; set; } = string.Empty;

    [StringLength(50)]
    [Display(Name = "Province")]
    public string? Province { get; set; }
}
