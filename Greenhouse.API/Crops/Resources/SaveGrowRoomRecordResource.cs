using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Crops.Resources;

public class SaveGrowRoomRecordResource
{
    [Required]
    public string Author { get; set; }
    
    [Required]
    public int GrowRoom { get; set; }
    
    [Required]
    public float AirTemperature { get; set; }
    
    [Required]
    public float CompostTemperature { get; set; }
    
    [Required]
    public float CarbonDioxide { get; set; }
    
    [Required]
    public float AirHumidity { get; set; }
    
    [Required]
    public float Setting { get; set; }

    public string? Comment { get; set; }
    
    [Required]
    public int EmployeeId { get; set; }
}