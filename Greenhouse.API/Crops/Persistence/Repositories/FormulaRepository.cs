using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Crops.Persistence.Repositories;

public class FormulaRepository : BaseRepository, IFormulaRepository
{
    public FormulaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Formula>> ListAsync()
    {
        return await _context.Formulas
            .ToListAsync();
    }

    public async Task AddAsync(Formula formula)
    {
        var currentDate = DateTime.Now;
        formula.Day = currentDate.Day - formula.Date.Day + 1;
        await _context.Formulas.AddAsync(formula);
    }

    public async Task<Formula> FindByIdAsync(int formulaId)
    {
        return await _context.Formulas
            .FirstOrDefaultAsync(p => p.Id == formulaId);
        
    }

    public async Task<IEnumerable<Formula>> FindByCropIdAsync(int cropId)
    {
        return await _context.Formulas
            .Where(p => p.CropId == cropId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Formula>> FindByEmployeeIdAsync(int employeeId)
    {
        return await _context.Formulas
            .Where(p => p.EmployeeId == employeeId)
            .ToListAsync();
    }

    public void Update(Formula formula)
    {
        _context.Formulas.Update(formula);
    }

    public void Remove(Formula formula)
    {
        _context.Formulas.Remove(formula);
    }
}