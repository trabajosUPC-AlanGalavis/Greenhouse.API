using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Models;

public class PreparationArea
{
    public int Id { get; set; }
    public string? Author { get; set; }
    public int Day { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string? Activities { get; set; }
    public float Temperature { get; set; }
    public string? Comment { get; set; }

    // Relationships
    public int CropId { get; set; }
    public Crop? Crop { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    public PreparationArea()
    {
        var currentDateTime = DateTime.Now;
        var currentDate = DateOnly.FromDateTime(currentDateTime);
        var currentTime = TimeOnly.FromDateTime(currentDateTime);
        Date = currentDate;
        Time = currentTime;
    }
}