using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Crops.Resources;

public class GrowRoomRecordResource
{
    public int Id { get; set; }
    public string Author { get; set; }
    public int Day { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public int GrowRoom { get; set; }
    public float AirTemperature { get; set; }
    public float CompostTemperature { get; set; }
    public float CarbonDioxide { get; set; }
    public float AirHumidity { get; set; }
    public float Setting { get; set; }
    public string ProcessType { get; set; }
    public string Comment { get; set; }
    public int CropId { get; set; }
}