using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Models;

public class GrowRoomRecord
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int GrowRoom { get; set; }
    public float AirTemperature { get; set; }
    public float CompostTemperature { get; set; }
    public float CarbonDioxide { get; set; }
    public float AirHumidity { get; set; }
    public float Setting { get; set; }
    public string ProcessType { get; set; }
    public string Comment { get; set; }
    
    // Relationships
    public int CropId { get; set; }
    public Crop Crop { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    
}