using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Crops.Persistence.Repositories;

public class PhaseRepository : BaseRepository, IPhaseRepository
{
    public PhaseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Phase>> ListAsync()
    {
        return await _context.Phases.ToListAsync();
    }

    public async Task AddAsync(Phase phase)
    {
        await _context.Phases.AddAsync(phase);
    }

    public async Task<Phase> FindByIdAsync(int id)
    {
        return await _context.Phases.FindAsync(id);
    }

    public void Update(Phase phase)
    {
        _context.Phases.Update(phase);
    }

    public void Remove(Phase phase)
    {
        _context.Phases.Remove(phase);
    }
}