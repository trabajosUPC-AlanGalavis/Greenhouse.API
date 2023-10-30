using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services;

public interface IFormulaService
{
    Task<IEnumerable<Formula>> ListAsync();
    Task<IEnumerable<Formula>> ListByCropIdAsync(int cropId);
    Task<IEnumerable<Formula>> ListByEmployeeIdAsync(int employeeId);
    Task<FormulaResponse> SaveAsync(Formula formula);
    Task<FormulaResponse> UpdateAsync(int formulaId, Formula formula);
    Task<FormulaResponse> DeleteAsync(int formulaId);
}