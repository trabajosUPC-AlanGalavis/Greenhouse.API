using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface IPreparationAreaService
{
    Task<IEnumerable<PreparationArea>> ListAsync();
    Task<IEnumerable<PreparationArea>> ListByCropIdAsync(int cropId);
    Task<IEnumerable<PreparationArea>> ListByEmployeeIdAsync(int employeeId);
    Task<PreparationAreaResponse> SaveAsync(PreparationArea preparationAreaRepository);
    Task<PreparationAreaResponse> UpdateAsync(int preparationAreaId, PreparationArea preparationAreaRepository);
    Task<PreparationAreaResponse> DeleteAsync(int preparationAreaId);
}