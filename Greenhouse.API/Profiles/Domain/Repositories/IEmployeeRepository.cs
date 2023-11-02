using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Profiles.Domain.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> ListAsync();
    Task AddAsync(Employee employee);
    void Update(Employee employee);
    void Remove(Employee employee);
    Task<Employee> FindByIdAsync(int employeeId);
    Task<Employee> FindByEmailAsync(string title);
    Task<IEnumerable<Employee>> FindByCompanyIdAsync(int companyId);
}