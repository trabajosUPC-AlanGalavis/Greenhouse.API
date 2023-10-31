using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Crops.Persistence.Repositories;

public class GrowRoomRecordRepository : BaseRepository, IGrowRoomRecordRepository
{
    public GrowRoomRecordRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<GrowRoomRecord>> ListAsync()
    {
        return await _context.GrowRoomRecords
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .ToListAsync();
    }

    public async Task AddAsync(GrowRoomRecord growRoomRecord)
    {
        await _context.GrowRoomRecords.AddAsync(growRoomRecord);
    }

    public async Task<GrowRoomRecord> FindByIdAsync(int growRoomRecordId)
    {
        return await _context.GrowRoomRecords
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .FirstOrDefaultAsync(p => p.Id == growRoomRecordId);
            
    }

    public async Task<IEnumerable<GrowRoomRecord>> FindByCropIdAsync(int cropId)
    {
        return await _context.GrowRoomRecords
            .Where(p => p.CropId == cropId)
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<GrowRoomRecord>> FindByEmployeeIdAsync(int employeeId)
    {
        return await _context.GrowRoomRecords
            .Where(p => p.EmployeeId == employeeId)
            .Include(p => p.Employee)
            .ToListAsync();
    }

    public void Update(GrowRoomRecord growRoomRecord)
    {
        _context.GrowRoomRecords.Update(growRoomRecord);
    }

    public void Remove(GrowRoomRecord growRoomRecord)
    {
        _context.GrowRoomRecords.Remove(growRoomRecord);
    }
}
