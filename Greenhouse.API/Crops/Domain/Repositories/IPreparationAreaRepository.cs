using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface IPreparationAreaRepository
{
    Task<IEnumerable<PreparationArea>> ListAsync();
    Task AddAsync(PreparationArea preparationAreaRepository);
    void Update(PreparationArea preparationAreaRepository);
    void Remove(PreparationArea preparationAreaRepository);
    Task<PreparationArea> FindByIdAsync(int preparationAreaId);
    Task<IEnumerable<PreparationArea>> FindByCropIdAsync(int cropId);
    Task<IEnumerable<PreparationArea>> FindByEmployeeIdAsync(int employeeId);
}