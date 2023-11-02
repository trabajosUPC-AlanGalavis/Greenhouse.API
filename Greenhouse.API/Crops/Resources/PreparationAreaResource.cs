using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Crops.Resources;

public class PreparationAreaResource
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Activities { get; set; }
    public float Temperature { get; set; }
    public string Comment { get; set; }
    public CropResource Crop { get; set; }
    public EmployeeResource Employee { get; set; }
}