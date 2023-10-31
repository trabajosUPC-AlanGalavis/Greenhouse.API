using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Crops.Resources;

public class SaveCropResource
{
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public bool State { get; set; }
    
    [Required]
    public int PhaseId { get; set; }
}