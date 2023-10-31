using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Services;
using Greenhouse.API.Profiles.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Profiles.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
    }

    public async Task<IEnumerable<Employee>> ListAsync()
    {
        return await _employeeRepository.ListAsync();
    }

    public async Task<IEnumerable<Employee>> ListByCompanyIdAsync(int companyId)
    {
        return await _employeeRepository.FindByCompanyIdAsync(companyId);
    }

    public async Task<EmployeeResponse> SaveAsync(Employee employee)
    {
        // Validate CompanyId

        var existingCompany = await _companyRepository.FindByIdAsync(employee.CompanyId);

        if (existingCompany == null)
            return new EmployeeResponse("Invalid Company");
        
        // Validate Email

        var existingEmployeeWithEmail = await _employeeRepository.FindByEmailAsync(employee.Email);

        if (existingEmployeeWithEmail != null)
            return new EmployeeResponse("Employee email already exists.");

        try
        {
            // Add Employee
            await _employeeRepository.AddAsync(employee);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new EmployeeResponse(employee);

        }
        catch (Exception e)
        {
            // Error Handling
            return new EmployeeResponse($"An error occurred while saving the employee: {e.Message}");
        }

        
    }

    public async Task<EmployeeResponse> UpdateAsync(int employeeId, Employee employee)
    {
        var existingEmployee = await _employeeRepository.FindByIdAsync(employeeId);
        
        // Validate Employee

        if (existingEmployee == null)
            return new EmployeeResponse("Employee not found.");

        // Validate CompanyId

        var existingCompany = await _companyRepository.FindByIdAsync(employee.CompanyId);

        if (existingCompany == null)
            return new EmployeeResponse("Invalid Company");
        
        // Validate Title

        var existingEmployeeWithEmail = await _employeeRepository.FindByEmailAsync(employee.Email);

        if (existingEmployeeWithEmail != null && existingEmployeeWithEmail.Id != existingEmployee.Id)
            return new EmployeeResponse("Employee email already exists.");
        
        // Modify Fields
        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.Password = employee.Password;

        try
        {
            _employeeRepository.Update(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return new EmployeeResponse(existingEmployee);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new EmployeeResponse($"An error occurred while updating the employee: {e.Message}");
        }
    }

    public async Task<EmployeeResponse> DeleteAsync(int employeeId)
    {
        var existingEmployee = await _employeeRepository.FindByIdAsync(employeeId);
        
        // Validate Employee

        if (existingEmployee == null)
            return new EmployeeResponse("Employee not found.");
        
        try
        {
            _employeeRepository.Remove(existingEmployee);
            await _unitOfWork.CompleteAsync();

            return new EmployeeResponse(existingEmployee);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new EmployeeResponse($"An error occurred while deleting the employee: {e.Message}");
        }

    }
}