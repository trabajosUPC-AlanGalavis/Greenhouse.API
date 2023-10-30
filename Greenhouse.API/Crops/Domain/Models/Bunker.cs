using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Models;

public class Bunker
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float ThermocoupleOne { get; set; }
    public float ThermocoupleTwo { get; set; }
    public float ThermocoupleThree { get; set; }
    public float AverageThermocouple { get; set; }
    public float MotorFrequency { get; set; }
    public string Comment { get; set; }

    // Relationships
    public int CropId { get; set; }
    public Crop Crop { get; set; }
    
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
}