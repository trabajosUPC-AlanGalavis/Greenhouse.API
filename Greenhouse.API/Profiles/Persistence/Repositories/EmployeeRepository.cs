using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Repositories;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Profiles.Persistence.Repositories;

public class EmployeeRepository : BaseRepository, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Employee>> ListAsync()
    {
        return await _context.Employees
            .Include(p => p.Company)
            .ToListAsync();
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
    }

    public void Remove(Employee employee)
    {
        _context.Employees.Remove(employee);
    }
    
    public async Task<Employee> FindByIdAsync(int employeeId)
    {
        return await _context.Employees
            .Include(p => p.Company)
            .FirstOrDefaultAsync(p => p.Id == employeeId);
        
    }

    public async Task<Employee> FindByEmailAsync(string email)
    {
        return await _context.Employees
            .Include(p => p.Company)
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<IEnumerable<Employee>> FindByCompanyIdAsync(int companyId)
    {
        return await _context.Employees
            .Where(p => p.CompanyId == companyId)
            .Include(p => p.Company)
            .ToListAsync();
    }
}