using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface IGrowRoomRecordService
{
    Task<IEnumerable<GrowRoomRecord>> ListAsync();
    Task<IEnumerable<GrowRoomRecord>> ListByCropIdAsync(int cropId);
    Task<IEnumerable<GrowRoomRecord>> ListByEmployeeIdAsync(int employeeId);
    Task<GrowRoomRecordResponse> SaveAsync(GrowRoomRecord growRoomRecord);
    Task<GrowRoomRecordResponse> UpdateAsync(int growRoomRecordId, GrowRoomRecord growRoomRecord);
    Task<GrowRoomRecordResponse> DeleteAsync(int growRoomRecordId);
}