using Greenhouse.API.Crops.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface ICropRepository
{
    Task<IEnumerable<Crop>> ListAsync();
    Task<IEnumerable<Crop>> ListByCompanyIdAsync(int companyId);
    Task AddAsync(Crop crop);
    Task<Crop> FindByIdAsync(int cropId);
    Task<Crop> FindByStateAsync(bool state);
    void Update(Crop crop);
    void Remove(Crop crop);
}