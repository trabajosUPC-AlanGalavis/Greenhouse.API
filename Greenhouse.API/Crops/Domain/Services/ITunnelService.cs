using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface ITunnelService
{
    Task<IEnumerable<Tunnel>> ListAsync();
    Task<IEnumerable<Tunnel>> ListByCropIdAsync(int cropId);
    Task<IEnumerable<Tunnel>> ListByEmployeeIdAsync(int employeeId);
    Task<TunnelResponse> SaveAsync(Tunnel tunnel);
    Task<TunnelResponse> UpdateAsync(int tunnelId, Tunnel tunnel);
    Task<TunnelResponse> DeleteAsync(int tunnelId);
}