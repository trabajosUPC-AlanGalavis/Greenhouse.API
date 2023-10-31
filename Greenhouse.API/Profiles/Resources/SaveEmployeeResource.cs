using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Profiles.Resources;

public class SaveEmployeeResource
{
    [Required]
    [MaxLength(250)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string LastName { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Password { get; set; }
    
    [Required]
    public int CompanyId { get; set; }
}