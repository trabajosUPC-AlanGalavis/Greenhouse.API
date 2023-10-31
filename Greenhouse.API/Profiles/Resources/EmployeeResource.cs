namespace Greenhouse.API.Profiles.Resources;

public class EmployeeResource
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public CompanyResource Company { get; set; }
}