using Greenhouse.API.Crops.Domain.Models;

namespace Greenhouse.API.Profiles.Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    // Relationships
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
    public IList<Formula> Formulas { get; set; } = new List<Formula>();
    public IList<PreparationArea> PreparationAreas { get; set; } = new List<PreparationArea>();
    public IList<Bunker> Bunkers { get; set; } = new List<Bunker>();
    public IList<Tunnel> Tunnels { get; set; } = new List<Tunnel>();
    public IList<GrowRoomRecord> GrowRoomRecords { get; set; } = new List<GrowRoomRecord>();
}