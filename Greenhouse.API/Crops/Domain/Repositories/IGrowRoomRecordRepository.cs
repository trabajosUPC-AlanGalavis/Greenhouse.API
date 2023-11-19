using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface IGrowRoomRecordRepository
{
    Task<IEnumerable<GrowRoomRecord>> ListAsync();
    Task AddAsync(GrowRoomRecord growRoomRecord);
    void Update(GrowRoomRecord growRoomRecord);
    void Remove(GrowRoomRecord growRoomRecord);
    Task<GrowRoomRecord> FindByIdAsync(int growRoomRecordId);
    Task<IEnumerable<GrowRoomRecord>> FindByCropIdAsync(int cropId);
    Task<IEnumerable<GrowRoomRecord>> FindByCropIdAndProcessTypeAsync(int cropId, string processType);
    Task<IEnumerable<GrowRoomRecord>> FindByEmployeeIdAsync(int employeeId);
}