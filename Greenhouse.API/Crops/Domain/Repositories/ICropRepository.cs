using Greenhouse.API.Crops.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface ICropRepository
{
    Task<IEnumerable<Crop>> ListAsync();
    Task AddAsync(Crop crop);
    Task<Crop> FindByIdAsync(int cropId);
    Task<Crop> FindByStateAsync(bool state);
    Task<IEnumerable<Crop>> FindByPhaseIdAsync(int phaseId);
    void Update(Crop crop);
    void Remove(Crop crop);
}