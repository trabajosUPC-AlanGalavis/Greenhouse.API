using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Profiles.Resources;

public class SaveCompanyResource
{
    [Required]
    [MaxLength(250)]
    public string CompanyName { get; set; }
    
    [Required]
    public long Tin { get; set; }
}