using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface ITunnelRepository
{
    Task<IEnumerable<Tunnel>> ListAsync();
    Task AddAsync(Tunnel tunnel);
    void Update(Tunnel tunnel);
    void Remove(Tunnel tunnel);
    Task<Tunnel> FindByIdAsync(int tunnelId);
    Task<IEnumerable<Tunnel>> FindByCropIdAsync(int cropId);
    Task<IEnumerable<Tunnel>> FindByEmployeeIdAsync(int employeeId);
}