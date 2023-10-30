using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Services.Communication;

namespace Greenhouse.API.Profiles.Domain.Services;

public interface ICompanyService
{
    Task<IEnumerable<Company>> ListAsync();
    Task<CompanyResponse> SaveAsync(Company company);
    Task<CompanyResponse> UpdateAsync(int id, Company company);
    Task<CompanyResponse> DeleteAsync(int id);
}