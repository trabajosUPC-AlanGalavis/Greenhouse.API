using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Crops.Resources;

public class GrowRoomRecordResource
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
    public CropResource Crop { get; set; }
    public EmployeeResource Employee { get; set; }
}