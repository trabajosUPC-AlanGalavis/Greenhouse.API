using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class PreparationAreaService : IPreparationAreaService
{
    private readonly IPreparationAreaRepository _preparationAreaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICropRepository _cropRepository;

    public PreparationAreaService(IPreparationAreaRepository preparationAreaRepository, IUnitOfWork unitOfWork, ICropRepository cropRepository)
    {
        _preparationAreaRepository = preparationAreaRepository;
        _unitOfWork = unitOfWork;
        _cropRepository = cropRepository;
    }

    public async Task<IEnumerable<PreparationArea>> ListAsync()
    {
        return await _preparationAreaRepository.ListAsync();
    }

    public async Task<IEnumerable<PreparationArea>> ListByCropIdAsync(int cropId)
    {
        return await _preparationAreaRepository.FindByCropIdAsync(cropId);
    }
    
    public async Task<IEnumerable<PreparationArea>> ListByEmployeeIdAsync(int employeeId)
    {
        return await _preparationAreaRepository.FindByEmployeeIdAsync(employeeId);
    }

    public async Task<PreparationAreaResponse> SaveAsync(PreparationArea preparationArea)
    {
        try
        {
            // Add PreparationArea
            await _preparationAreaRepository.AddAsync(preparationArea);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new PreparationAreaResponse(preparationArea);

        }
        catch (Exception e)
        {
            // Error Handling
            return new PreparationAreaResponse($"An error occurred while saving the preparation area: {e.Message}");
        }

        
    }

    public async Task<PreparationAreaResponse> UpdateAsync(int preparationAreaId, PreparationArea preparationArea)
    {
        var existingPreparationArea = await _preparationAreaRepository.FindByIdAsync(preparationAreaId);
        
        // Validate PreparationArea

        if (existingPreparationArea == null)
            return new PreparationAreaResponse("Preparation area not found.");

        // Validate CropId

        var existingCrop = await _cropRepository.FindByIdAsync(preparationArea.CropId);

        if (existingCrop == null)
            return new PreparationAreaResponse("Invalid Crop");
        
        // Modify Fields
        existingPreparationArea.Date= preparationArea.Date;
        existingPreparationArea.Activities = preparationArea.Activities;
        existingPreparationArea.Temperature = preparationArea.Temperature;
        existingPreparationArea.Comment = preparationArea.Comment;

        try
        {
            _preparationAreaRepository.Update(existingPreparationArea);
            await _unitOfWork.CompleteAsync();

            return new PreparationAreaResponse(existingPreparationArea);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new PreparationAreaResponse($"An error occurred while updating the preparation area: {e.Message}");
        }
    }

    public async Task<PreparationAreaResponse> DeleteAsync(int preparationAreaId)
    {
        var existingPreparationArea = await _preparationAreaRepository.FindByIdAsync(preparationAreaId);
        
        // Validate PreparationArea

        if (existingPreparationArea == null)
            return new PreparationAreaResponse("Preparation area not found.");
        
        try
        {
            _preparationAreaRepository.Remove(existingPreparationArea);
            await _unitOfWork.CompleteAsync();

            return new PreparationAreaResponse(existingPreparationArea);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new PreparationAreaResponse($"An error occurred while deleting the preparation area: {e.Message}");
        }

    }
}

