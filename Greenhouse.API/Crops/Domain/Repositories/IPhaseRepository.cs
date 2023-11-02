using Greenhouse.API.Crops.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface IPhaseRepository
{
    Task<IEnumerable<Phase>> ListAsync();
    Task AddAsync(Phase phase);
    Task<Phase> FindByIdAsync(int id);
    void Update(Phase phase);
    void Remove(Phase phase);

}