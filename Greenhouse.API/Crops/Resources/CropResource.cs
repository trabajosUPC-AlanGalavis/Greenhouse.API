namespace Greenhouse.API.Crops.Resources;

public class CropResource
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool State { get; set; }
    public PhaseResource Phase { get; set; }
}