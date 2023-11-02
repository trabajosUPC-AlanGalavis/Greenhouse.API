using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Crops.Resources;

public class SavePhaseResource
{
    [Required]
    [MaxLength(250)]
    public string Name { get; set; }
}