using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class BunkerService : IBunkerService
{
    private readonly IBunkerRepository _bunkerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICropRepository _cropRepository;

    public BunkerService(IBunkerRepository bunkerRepository, IUnitOfWork unitOfWork, ICropRepository cropRepository)
    {
        _bunkerRepository = bunkerRepository;
        _unitOfWork = unitOfWork;
        _cropRepository = cropRepository;
    }

    public async Task<IEnumerable<Bunker>> ListAsync()
    {
        return await _bunkerRepository.ListAsync();
    }

    public async Task<IEnumerable<Bunker>> ListByCropIdAsync(int cropId)
    {
        return await _bunkerRepository.FindByCropIdAsync(cropId);
    }
    
    public async Task<IEnumerable<Bunker>> ListByEmployeeIdAsync(int employeeId)
    {
        return await _bunkerRepository.FindByEmployeeIdAsync(employeeId);
    }

    public async Task<BunkerResponse> SaveAsync(Bunker bunker)
    {
        try
        {
            // Add Bunker
            await _bunkerRepository.AddAsync(bunker);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new BunkerResponse(bunker);

        }
        catch (Exception e)
        {
            // Error Handling
            return new BunkerResponse($"An error occurred while saving the bunker: {e.Message}");
        }

        
    }

    public async Task<BunkerResponse> UpdateAsync(int bunkerId, Bunker bunker)
    {
        var existingBunker = await _bunkerRepository.FindByIdAsync(bunkerId);
        
        // Validate Bunker

        if (existingBunker == null)
            return new BunkerResponse("Bunker not found.");

        // Validate CropId

        var existingCrop = await _cropRepository.FindByIdAsync(bunker.CropId);

        if (existingCrop == null)
            return new BunkerResponse("Invalid Crop");
        
        // Modify Fields
        existingBunker.Date= bunker.Date;
        existingBunker.ThermocoupleOne = bunker.ThermocoupleOne;
        existingBunker.ThermocoupleTwo = bunker.ThermocoupleTwo;
        existingBunker.ThermocoupleThree = bunker.ThermocoupleThree;
        existingBunker.AverageThermocouple = bunker.AverageThermocouple;
        existingBunker.MotorFrequency = bunker.MotorFrequency;
        existingBunker.Comment = bunker.Comment;

        try
        {
            _bunkerRepository.Update(existingBunker);
            await _unitOfWork.CompleteAsync();

            return new BunkerResponse(existingBunker);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new BunkerResponse($"An error occurred while updating the bunker: {e.Message}");
        }
    }

    public async Task<BunkerResponse> DeleteAsync(int bunkerId)
    {
        var existingBunker = await _bunkerRepository.FindByIdAsync(bunkerId);
        
        // Validate Bunker

        if (existingBunker == null)
            return new BunkerResponse("Bunker not found.");
        
        try
        {
            _bunkerRepository.Remove(existingBunker);
            await _unitOfWork.CompleteAsync();

            return new BunkerResponse(existingBunker);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new BunkerResponse($"An error occurred while deleting the bunker: {e.Message}");
        }

    }
}