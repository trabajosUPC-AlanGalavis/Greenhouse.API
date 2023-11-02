using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Crops.Resources;

public class FormulaResource
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public float Hay { get; set; }
    public float Corn { get; set; }
    public float Guano { get; set; }
    public float CottonSeedCake { get; set; }
    public float SoybeanMeal { get; set; }
    public float Gypsum { get; set; }
    public float Urea { get; set; }
    public float AmmoniumSulphate { get; set; }
    public CropResource Crop { get; set; }
    public EmployeeResource Employee { get; set; }
}