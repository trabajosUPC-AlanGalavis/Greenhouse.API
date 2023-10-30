namespace Greenhouse.API.Crops.Domain.Models;

public class Crop
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool State { get; set; }
    
    // Relationships
    public int PhaseId { get; set; }
    public Phase Phase { get; set; }
    public IList<Formula> Formulas { get; set; } = new List<Formula>();
    public IList<PreparationArea> PreparationAreas { get; set; } = new List<PreparationArea>();
    public IList<Bunker> Bunkers { get; set; } = new List<Bunker>();
    public IList<Tunnel> Tunnels { get; set; } = new List<Tunnel>();
    public IList<GrowRoomRecord> GrowRoomRecords { get; set; } = new List<GrowRoomRecord>();
}