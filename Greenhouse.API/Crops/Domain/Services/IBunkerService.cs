using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface IBunkerService
{
    Task<IEnumerable<Bunker>> ListAsync();
    Task<IEnumerable<Bunker>> ListByCropIdAsync(int cropId);
    Task<IEnumerable<Bunker>> ListByEmployeeIdAsync(int employeeId);
    Task<BunkerResponse> SaveAsync(Bunker bunker);
    Task<BunkerResponse> UpdateAsync(int bunkerId, Bunker bunker);
    Task<BunkerResponse> DeleteAsync(int bunkerId);
}