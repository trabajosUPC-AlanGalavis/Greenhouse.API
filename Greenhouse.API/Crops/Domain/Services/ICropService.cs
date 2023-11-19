using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface ICropService
{
    Task<IEnumerable<Crop>> ListAsync();
    Task<IEnumerable<Crop>> ListByCompanyIdAsync(int companyId);
    //get by Id
    Task<Crop> GetByIdAsync(int cropId);
    Task<CropResponse> SaveAsync(Crop crop);
    Task<CropResponse> UpdateAsync(int cropId, Crop crop);
    Task<CropResponse> DeleteAsync(int cropId);
}