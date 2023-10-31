using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Services.Communication;

namespace Greenhouse.API.Profiles.Domain.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> ListAsync();
    Task<IEnumerable<Employee>> ListByCompanyIdAsync(int companyId);
    Task<EmployeeResponse> SaveAsync(Employee employee);
    Task<EmployeeResponse> UpdateAsync(int employeeId, Employee employee);
    Task<EmployeeResponse> DeleteAsync(int employeeId);
}