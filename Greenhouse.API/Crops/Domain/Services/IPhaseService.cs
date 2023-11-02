using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface IPhaseService
{
    Task<IEnumerable<Phase>> ListAsync();
    Task<PhaseResponse> SaveAsync(Phase phase);
    Task<PhaseResponse> UpdateAsync(int id, Phase phase);
    Task<PhaseResponse> DeleteAsync(int id);
}