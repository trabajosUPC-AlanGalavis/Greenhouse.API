using Greenhouse.API.Crops.Domain.Models;

namespace Greenhouse.API.Crops.Resources;

public class CropResource
{
    public int Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool State { get; set; }
    public string Phase { get; set; }
    public int CompanyId { get; set; }
}