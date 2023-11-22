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
            .ToListAsync();
    }

    public async Task<IEnumerable<Crop>> ListByCompanyIdAsync(int companyId)
    {
        return await _context.Crops
            .Where(p => p.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task AddAsync(Crop crop)
    {
        crop.Phase = "Formula";
        crop.State = true;
        var currentDate = DateTime.Now;
        crop.StartDate = DateOnly.FromDateTime(currentDate);
        crop.EndDate = DateOnly.FromDateTime(currentDate);
        await _context.Crops.AddAsync(crop);
    }

    public async Task<Crop> FindByIdAsync(int cropId)
    {
        return await _context.Crops
            .FirstOrDefaultAsync(p => p.Id == cropId);
        
    }

    public async Task<Crop> FindByStateAsync(bool state)
    {
        return await _context.Crops
            .FirstOrDefaultAsync(p => p.State == state);
    }

    public void Update(Crop crop)
    {
        switch (crop.Phase)
        {
            case "Formula":
                crop.Phase = "Preparation Area";
                break;
            case "Preparation Area":
                crop.Phase = "Bunker";
                break;
            case "Bunker":
                crop.Phase = "Tunnel";
                break;
            case "Tunnel":
                crop.Phase = "Incubation";
                break;
            case "Incubation":
                crop.Phase = "Casing";
                break;
            case "Casing":
                crop.Phase = "Induction";
                break;
            case "Induction":
                crop.Phase = "Harvest";
                break;
            case "Harvest":
                crop.State = false;
                var currentDate = DateTime.Now;
                crop.EndDate = DateOnly.FromDateTime(currentDate);
                break;
        }
        _context.Crops.Update(crop);
    }

    public void Remove(Crop crop)
    {
        _context.Crops.Remove(crop);
    }
}