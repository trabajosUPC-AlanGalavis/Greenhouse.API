using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Crops.Resources;

public class PreparationAreaResource
{
    public int Id { get; set; }
    public string Author { get; set; }
    public int Day { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Activities { get; set; }
    public float Temperature { get; set; }
    public string Comment { get; set; }
    public int CropId { get; set; }
}