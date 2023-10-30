namespace Greenhouse.API.Profiles.Domain.Models;

public class Company
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public long Tin { get; set; }
    
    // Relationships
    
    public IList<Employee> Employees { get; set; } = new List<Employee>();
    
}