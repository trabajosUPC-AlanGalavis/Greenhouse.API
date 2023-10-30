using Greenhouse.API.Profiles.Domain.Models;

namespace Greenhouse.API.Profiles.Domain.Repositories;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> ListAsync();
    Task AddAsync(Company company);
    void Update(Company company);
    void Remove(Company company);
    Task<Company> FindByIdAsync(int id);
}