using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class FormulaService : IFormulaService
{
    private readonly IFormulaRepository _formulaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICropRepository _cropRepository;

    public FormulaService(IFormulaRepository formulaRepository, IUnitOfWork unitOfWork, ICropRepository cropRepository)
    {
        _formulaRepository = formulaRepository;
        _unitOfWork = unitOfWork;
        _cropRepository = cropRepository;
    }

    public async Task<IEnumerable<Formula>> ListAsync()
    {
        return await _formulaRepository.ListAsync();
    }

    public async Task<IEnumerable<Formula>> ListByCropIdAsync(int cropId)
    {
        return await _formulaRepository.FindByCropIdAsync(cropId);
    }
    
    public async Task<IEnumerable<Formula>> ListByEmployeeIdAsync(int employeeId)
    {
        return await _formulaRepository.FindByEmployeeIdAsync(employeeId);
    }

    public async Task<FormulaResponse> SaveAsync(Formula formula)
    {
        try
        {
            // Add Formula
            await _formulaRepository.AddAsync(formula);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new FormulaResponse(formula);

        }
        catch (Exception e)
        {
            // Error Handling
            return new FormulaResponse($"An error occurred while saving the formula: {e.Message}");
        }

        
    }

    public async Task<FormulaResponse> UpdateAsync(int formulaId, Formula formula)
    {
        var existingFormula = await _formulaRepository.FindByIdAsync(formulaId);
        
        // Validate Formula

        if (existingFormula == null)
            return new FormulaResponse("Formula not found.");

        // Validate CropId

        var existingCrop = await _cropRepository.FindByIdAsync(formula.CropId);

        if (existingCrop == null)
            return new FormulaResponse("Invalid Crop");
        
        // Modify Fields
        existingFormula.Date= formula.Date;
        existingFormula.Hay = formula.Hay;
        existingFormula.Corn = formula.Corn;
        existingFormula.Guano = formula.Guano;
        existingFormula.CottonSeedCake = formula.CottonSeedCake;
        existingFormula.SoybeanMeal = formula.SoybeanMeal;
        existingFormula.Gypsum = formula.Gypsum;
        existingFormula.Urea = formula.Urea;
        existingFormula.AmmoniumSulphate = formula.AmmoniumSulphate;

        try
        {
            _formulaRepository.Update(existingFormula);
            await _unitOfWork.CompleteAsync();

            return new FormulaResponse(existingFormula);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new FormulaResponse($"An error occurred while updating the formula: {e.Message}");
        }
    }

    public async Task<FormulaResponse> DeleteAsync(int formulaId)
    {
        var existingFormula = await _formulaRepository.FindByIdAsync(formulaId);
        
        // Validate Formula

        if (existingFormula == null)
            return new FormulaResponse("Formula not found.");
        
        try
        {
            _formulaRepository.Remove(existingFormula);
            await _unitOfWork.CompleteAsync();

            return new FormulaResponse(existingFormula);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new FormulaResponse($"An error occurred while deleting the formula: {e.Message}");
        }

    }
}

