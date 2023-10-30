using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Repositories;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Profiles.Persistence.Repositories;

public class CompanyRepository : BaseRepository, ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Company>> ListAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public async Task<Company> FindByIdAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }

    public void Update(Company company)
    {
        _context.Companies.Update(company);
    }

    public void Remove(Company company)
    {
        _context.Companies.Remove(company);
    }
}