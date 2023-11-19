using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Shared.Persistence.Contexts;
using Greenhouse.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Greenhouse.API.Crops.Persistence.Repositories;

public class TunnelRepository : BaseRepository, ITunnelRepository
{
    public TunnelRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tunnel>> ListAsync()
    {
        return await _context.Tunnels
            .ToListAsync();
    }

    public async Task AddAsync(Tunnel tunnel)
    {
        var currentDate = DateTime.Now;
        tunnel.Day = currentDate.Day - tunnel.Date.Day + 1;
        tunnel.AverageThermocouple = (tunnel.ThermocoupleOne + tunnel.ThermocoupleTwo + tunnel.ThermocoupleThree) / 3;
        await _context.Tunnels.AddAsync(tunnel);
    }

    public async Task<Tunnel> FindByIdAsync(int tunnelId)
    {
        return await _context.Tunnels
            .FirstOrDefaultAsync(p => p.Id == tunnelId);
        
    }

    public async Task<IEnumerable<Tunnel>> FindByCropIdAsync(int cropId)
    {
        return await _context.Tunnels
            .Where(p => p.CropId == cropId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Tunnel>> FindByEmployeeIdAsync(int employeeId)
    {
        return await _context.Tunnels
            .Where(p => p.EmployeeId == employeeId)
            .Include(p => p.Crop)
            .Include(p => p.Employee)
            .ToListAsync();
    }

    public void Update(Tunnel tunnel)
    {
        _context.Tunnels.Update(tunnel);
    }

    public void Remove(Tunnel tunnel)
    {
        _context.Tunnels.Remove(tunnel);
    }
}