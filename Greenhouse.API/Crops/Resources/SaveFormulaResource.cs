using System.ComponentModel.DataAnnotations;

namespace Greenhouse.API.Crops.Resources;

public class SaveFormulaResource
{
    [Required]
    public string Author { get; set; }
    
    //[Required]
    //public TimeSpan Time { get; set; }
    
    //[Required]
    //public DateOnly Date { get; set; }
    
    [Required]
    public float Hay { get; set; }
    
    [Required]
    public float Corn { get; set; }
    
    [Required]
    public float Guano { get; set; }
    
    [Required]
    public float CottonSeedCake { get; set; }
    
    [Required]
    public float SoybeanMeal { get; set; }
    
    [Required]
    public float Gypsum { get; set; }
    
    [Required]
    public float Urea { get; set; }
    
    [Required]
    public float AmmoniumSulphate { get; set; }

    [Required] public int EmployeeId { get; set; }
}