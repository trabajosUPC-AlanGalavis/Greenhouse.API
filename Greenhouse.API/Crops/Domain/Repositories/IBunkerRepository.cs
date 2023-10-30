using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface IBunkerRepository
{
    Task<IEnumerable<Bunker>> ListAsync();
    Task AddAsync(Bunker bunker);
    void Update(Bunker bunker);
    void Remove(Bunker bunker);
    Task<Bunker> FindByIdAsync(int bunkerId);
    Task<IEnumerable<Bunker>> FindByCropIdAsync(int cropId);
    Task<IEnumerable<Bunker>> FindByEmployeeIdAsync(int employeeId);
}