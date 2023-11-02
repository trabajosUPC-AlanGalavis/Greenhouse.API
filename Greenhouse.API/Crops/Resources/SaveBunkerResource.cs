using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Crops.Resources;

public class SaveBunkerResource
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public float ThermocoupleOne { get; set; }
    
    [Required]
    public float ThermocoupleTwo { get; set; }
    
    [Required]
    public float ThermocoupleThree { get; set; }
    
    [Required]
    public float AverageThermocouple { get; set; }
    
    [Required]
    public float MotorFrequency { get; set; }
    
    public string Comment { get; set; }
    
    [Required]
    public int CropId { get; set; }
    
    [Required]
    public int EmployeeId { get; set; }
}