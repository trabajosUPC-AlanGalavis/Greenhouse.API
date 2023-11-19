using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Crops.Persistence.Repositories;

public class PreparationAreaRepository : BaseRepository, IPreparationAreaRepository
{
    public PreparationAreaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PreparationArea>> ListAsync()
    {
        return await _context.PreparationAreas
            .ToListAsync();
    }

    public async Task AddAsync(PreparationArea preparationArea)
    {
        var currentDate = DateTime.Now;
        preparationArea.Day = currentDate.Day - preparationArea.Date.Day + 1;
        await _context.PreparationAreas.AddAsync(preparationArea);
    }

    public async Task<PreparationArea> FindByIdAsync(int preparationAreaId)
    {
        return await _context.PreparationAreas
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .FirstOrDefaultAsync(p => p.Id == preparationAreaId);
        
    }

    public async Task<IEnumerable<PreparationArea>> FindByCropIdAsync(int cropId)
    {
        return await _context.PreparationAreas
            .Where(p => p.CropId == cropId)
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<PreparationArea>> FindByEmployeeIdAsync(int employeeId)
    {
        return await _context.PreparationAreas
            .Where(p => p.EmployeeId == employeeId)
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .ToListAsync();
    }

    public void Update(PreparationArea preparationArea)
    {
        _context.PreparationAreas.Update(preparationArea);
    }

    public void Remove(PreparationArea preparationArea)
    {
        _context.PreparationAreas.Remove(preparationArea);
    }
}