using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class CropService : ICropService
{
    private readonly ICropRepository _cropRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhaseRepository _phaseRepository;

    public CropService(ICropRepository cropRepository, IUnitOfWork unitOfWork, IPhaseRepository phaseRepository)
    {
        _cropRepository = cropRepository;
        _unitOfWork = unitOfWork;
        _phaseRepository = phaseRepository;
    }

    public async Task<IEnumerable<Crop>> ListAsync()
    {
        return await _cropRepository.ListAsync();
    }

    public async Task<IEnumerable<Crop>> ListByPhaseIdAsync(int phaseId)
    {
        return await _cropRepository.FindByPhaseIdAsync(phaseId);
    }
    
    public async Task<Crop> ListByStateAsync(bool state)
    {
        return await _cropRepository.FindByStateAsync(state);
    }

    public async Task<CropResponse> SaveAsync(Crop crop)
    {
        try
        {
            // Add Crop
            await _cropRepository.AddAsync(crop);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new CropResponse(crop);

        }
        catch (Exception e)
        {
            // Error Handling
            return new CropResponse($"An error occurred while saving the crop: {e.Message}");
        }

        
    }

    public async Task<CropResponse> UpdateAsync(int cropId, Crop crop)
    {
        var existingCrop = await _cropRepository.FindByIdAsync(cropId);
        
        // Validate Crop

        if (existingCrop == null)
            return new CropResponse("Crop not found.");

        // Validate PhaseId

        var existingPhase = await _phaseRepository.FindByIdAsync(crop.PhaseId);

        if (existingPhase == null)
            return new CropResponse("Invalid Phase");
        
        // Modify Fields
        existingCrop.StartDate= crop.StartDate;
        existingCrop.EndDate = crop.EndDate;
        existingCrop.State = crop.State;

        try
        {
            _cropRepository.Update(existingCrop);
            await _unitOfWork.CompleteAsync();

            return new CropResponse(existingCrop);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new CropResponse($"An error occurred while updating the crop: {e.Message}");
        }
    }

    public async Task<CropResponse> DeleteAsync(int cropId)
    {
        var existingCrop = await _cropRepository.FindByIdAsync(cropId);
        
        // Validate Crop

        if (existingCrop == null)
            return new CropResponse("Crop not found.");
        
        try
        {
            _cropRepository.Remove(existingCrop);
            await _unitOfWork.CompleteAsync();

            return new CropResponse(existingCrop);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new CropResponse($"An error occurred while deleting the crop: {e.Message}");
        }

    }
}