using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Crops.Persistence.Repositories;

public class CropRepository : BaseRepository, ICropRepository
{
    public CropRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Crop>> ListAsync()
    {
        return await _context.Crops
            .Include(p => p.Phase)
            .ToListAsync();
    }

    public async Task AddAsync(Crop crop)
    {
        await _context.Crops.AddAsync(crop);
    }

    public async Task<Crop> FindByIdAsync(int cropId)
    {
        return await _context.Crops
            .Include(p => p.Phase)
            .FirstOrDefaultAsync(p => p.Id == cropId);
        
    }

    public async Task<Crop> FindByStateAsync(bool state)
    {
        return await _context.Crops
            .Include(p => p.Phase)
            .FirstOrDefaultAsync(p => p.State == state);
    }

    public async Task<IEnumerable<Crop>> FindByPhaseIdAsync(int phaseId)
    {
        return await _context.Crops
            .Where(p => p.PhaseId == phaseId)
            .Include(p => p.Phase)
            .ToListAsync();
    }

    public void Update(Crop crop)
    {
        _context.Crops.Update(crop);
    }

    public void Remove(Crop crop)
    {
        _context.Crops.Remove(crop);
    }
}