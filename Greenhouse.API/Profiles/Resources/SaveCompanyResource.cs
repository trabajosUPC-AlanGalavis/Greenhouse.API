using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Profiles.Resources;

public class SaveCompanyResource
{
    [Required]
    [MaxLength(250)]
    public string CompanyName { get; set; }
    
    [Required]
    public long Tin { get; set; }
}"feat(Company): Added CompanyResource and SaveCompanyResource classes for defining data transfer objects."