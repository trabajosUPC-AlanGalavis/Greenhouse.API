using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Repositories;
using Greenhouse.API.Profiles.Domain.Services;
using Greenhouse.API.Profiles.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Profiles.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
    {
        _companyRepository = companyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Company>> ListAsync()
    {
        return await _companyRepository.ListAsync();
    }

    public async Task<CompanyResponse> SaveAsync(Company company)
    {
        try
        {
            await _companyRepository.AddAsync(company);
            await _unitOfWork.CompleteAsync();
            return new CompanyResponse(company);
        }
        catch (Exception e)
        {
            return new CompanyResponse($"An error occurred while saving the company: {e.Message}");
        }
    }

    public async Task<CompanyResponse> UpdateAsync(int id, Company company)
    {
        var existingCompany = await _companyRepository.FindByIdAsync(id);

        if (existingCompany == null)
            return new CompanyResponse("Company not found.");

        existingCompany.CompanyName = company.CompanyName;
        existingCompany.Tin = company.Tin;

        try
        {
            _companyRepository.Update(existingCompany);
            await _unitOfWork.CompleteAsync();

            return new CompanyResponse(existingCompany);
        }
        catch (Exception e)
        {
            return new CompanyResponse($"An error occurred while updating the company: {e.Message}");
        }
    }

    public async Task<CompanyResponse> DeleteAsync(int id)
    {
        var existingCompany = await _companyRepository.FindByIdAsync(id);

        if (existingCompany == null)
            return new CompanyResponse("Company not found.");

        try
        {
            _companyRepository.Remove(existingCompany);
            await _unitOfWork.CompleteAsync();

            return new CompanyResponse(existingCompany);
        }
        catch (Exception e)
        {
            // Error Handling
            return new CompanyResponse($"An error occurred while deleting the company: {e.Message}");
        }
    }
}