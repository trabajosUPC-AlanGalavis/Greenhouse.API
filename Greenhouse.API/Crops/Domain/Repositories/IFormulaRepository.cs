using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Crops.Domain.Repositories;

public interface IFormulaRepository
{
    Task<IEnumerable<Formula>> ListAsync();
    Task AddAsync(Formula formula);
    void Update(Formula formula);
    void Remove(Formula formula);
    Task<Formula> FindByIdAsync(int formulaId);
    Task<IEnumerable<Formula>> FindByCropIdAsync(int cropId);
    Task<IEnumerable<Formula>> FindByEmployeeIdAsync(int employeeId);
}