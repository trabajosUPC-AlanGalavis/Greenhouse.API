using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Models;

public class PreparationArea
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Activities { get; set; }
    public float Temperature { get; set; }
    public string Comment { get; set; }

    // Relationships
    public int CropId { get; set; }
    public Crop Crop { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}