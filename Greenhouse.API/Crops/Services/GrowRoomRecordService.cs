using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class GrowRoomRecordService: IGrowRoomRecordService
{
    private readonly IGrowRoomRecordRepository _growRoomRecordRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICropRepository _cropRepository;

    public GrowRoomRecordService(IGrowRoomRecordRepository growRoomRecordRepository, IUnitOfWork unitOfWork, ICropRepository cropRepository)
    {
        _growRoomRecordRepository = growRoomRecordRepository;
        _unitOfWork = unitOfWork;
        _cropRepository = cropRepository;
    }

    public async Task<IEnumerable<GrowRoomRecord>> ListAsync()
    {
        return await _growRoomRecordRepository.ListAsync();
    }

    public async Task<IEnumerable<GrowRoomRecord>> ListByCropIdAsync(int cropId)
    {
        return await _growRoomRecordRepository.FindByCropIdAsync(cropId);
    }
    
    public async Task<IEnumerable<GrowRoomRecord>> ListByEmployeeIdAsync(int employeeId)
    {
        return await _growRoomRecordRepository.FindByEmployeeIdAsync(employeeId);
    }

    public async Task<GrowRoomRecordResponse> SaveAsync(GrowRoomRecord growRoomRecord)
    {
        try
        {
            // Add Formula
            await _growRoomRecordRepository.AddAsync(growRoomRecord);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new GrowRoomRecordResponse(growRoomRecord);

        }
        catch (Exception e)
        {
            // Error Handling
            return new GrowRoomRecordResponse($"An error occurred while saving the grow room record: {e.Message}");
        }

        
    }

    public async Task<GrowRoomRecordResponse> UpdateAsync(int formulaId, GrowRoomRecord growRoomRecord)
    {
        var existingGrowRoomRecord = await _growRoomRecordRepository.FindByIdAsync(formulaId);
        
        // Validate Formula

        if (existingGrowRoomRecord == null)
            return new GrowRoomRecordResponse("Grow room record not found.");

        // Validate CropId

        var existingCrop = await _cropRepository.FindByIdAsync(growRoomRecord.CropId);

        if (existingCrop == null)
            return new GrowRoomRecordResponse("Invalid Crop");
        
        // Modify Fields
        existingGrowRoomRecord.Date= growRoomRecord.Date;
        existingGrowRoomRecord.GrowRoom= growRoomRecord.GrowRoom;
        existingGrowRoomRecord.AirTemperature = growRoomRecord.AirTemperature;
        existingGrowRoomRecord.CompostTemperature = growRoomRecord.CompostTemperature;
        existingGrowRoomRecord.CarbonDioxide = growRoomRecord.CarbonDioxide;
        existingGrowRoomRecord.AirHumidity = growRoomRecord.AirHumidity;
        existingGrowRoomRecord.Setting = growRoomRecord.Setting;
        existingGrowRoomRecord.ProcessType = growRoomRecord.ProcessType;
        existingGrowRoomRecord.Comment = growRoomRecord.Comment;


        try
        {
            _growRoomRecordRepository.Update(existingGrowRoomRecord);
            await _unitOfWork.CompleteAsync();

            return new GrowRoomRecordResponse(existingGrowRoomRecord);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new GrowRoomRecordResponse($"An error occurred while updating the grow room record: {e.Message}");
        }
    }

    public async Task<GrowRoomRecordResponse> DeleteAsync(int growRoomRecordId)
    {
        var existingGrowRoomRecord = await _growRoomRecordRepository.FindByIdAsync(growRoomRecordId);
        
        // Validate Formula

        if (existingGrowRoomRecord == null)
            return new GrowRoomRecordResponse("Grow room record not found.");
        
        try
        {
            _growRoomRecordRepository.Remove(existingGrowRoomRecord);
            await _unitOfWork.CompleteAsync();

            return new GrowRoomRecordResponse(existingGrowRoomRecord);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new GrowRoomRecordResponse($"An error occurred while deleting the grow room record: {e.Message}");
        }

    }
}
