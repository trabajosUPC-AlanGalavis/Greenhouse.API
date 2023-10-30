namespace Greenhouse.API.Crops.Domain.Models;

public class Phase
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Relationships
    
    public IList<Crop> Crops { get; set; } = new List<Crop>();
    
}
