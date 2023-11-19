using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Models;

public class Formula
{
    public int Id { get; set; }
    
    public string? Author { get; set; }
    public int Day { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public float Hay { get; set; }
    public float Corn { get; set; }
    public float Guano { get; set; }
    public float CottonSeedCake { get; set; }
    public float SoybeanMeal { get; set; }
    public float Gypsum { get; set; }
    public float Urea { get; set; }
    public float AmmoniumSulphate { get; set; }

    // Relationships
    public int CropId { get; set; }
    public Crop? Crop { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    
    // Calculate Date and Time inside the constructor
    public Formula()
    {
        var currentDateTime = DateTime.Now;
        var currentDate = DateOnly.FromDateTime(currentDateTime);
        var currentTime = TimeOnly.FromDateTime(currentDateTime);
        Date = currentDate;
        Time = currentTime;
    }
    
}