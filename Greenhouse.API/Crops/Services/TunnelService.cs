using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class TunnelService : ITunnelService
{
    private readonly ITunnelRepository _tunnelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICropRepository _cropRepository;

    public TunnelService(ITunnelRepository tunnelRepository, IUnitOfWork unitOfWork, ICropRepository cropRepository)
    {
        _tunnelRepository = tunnelRepository;
        _unitOfWork = unitOfWork;
        _cropRepository = cropRepository;
    }

    public async Task<IEnumerable<Tunnel>> ListAsync()
    {
        return await _tunnelRepository.ListAsync();
    }

    public async Task<IEnumerable<Tunnel>> ListByCropIdAsync(int cropId)
    {
        return await _tunnelRepository.FindByCropIdAsync(cropId);
    }
    
    public async Task<IEnumerable<Tunnel>> ListByEmployeeIdAsync(int employeeId)
    {
        return await _tunnelRepository.FindByEmployeeIdAsync(employeeId);
    }

    public async Task<TunnelResponse> SaveAsync(Tunnel tunnel)
    {
        try
        {
            // Add Tunnel
            await _tunnelRepository.AddAsync(tunnel);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new TunnelResponse(tunnel);

        }
        catch (Exception e)
        {
            // Error Handling
            return new TunnelResponse($"An error occurred while saving the tunnel: {e.Message}");
        }

        
    }

    public async Task<TunnelResponse> UpdateAsync(int tunnelId, Tunnel tunnel)
    {
        var existingTunnel = await _tunnelRepository.FindByIdAsync(tunnelId);
        
        // Validate Tunnel

        if (existingTunnel == null)
            return new TunnelResponse("Tunnel not found.");

        // Validate CropId

        var existingCrop = await _cropRepository.FindByIdAsync(tunnel.CropId);

        if (existingCrop == null)
            return new TunnelResponse("Invalid Crop");
        
        // Modify Fields
        existingTunnel.Date= tunnel.Date;
        existingTunnel.ThermocoupleOne = tunnel.ThermocoupleOne;
        existingTunnel.ThermocoupleTwo = tunnel.ThermocoupleTwo;
        existingTunnel.ThermocoupleThree = tunnel.ThermocoupleThree;
        existingTunnel.AverageThermocouple = tunnel.AverageThermocouple;
        existingTunnel.RoomTemperature = tunnel.RoomTemperature;
        existingTunnel.MotorFrequency = tunnel.MotorFrequency;
        existingTunnel.FreshAir = tunnel.FreshAir;
        existingTunnel.Recirculation = tunnel.Recirculation;
        existingTunnel.Comment = tunnel.Comment;

        try
        {
            _tunnelRepository.Update(existingTunnel);
            await _unitOfWork.CompleteAsync();

            return new TunnelResponse(existingTunnel);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new TunnelResponse($"An error occurred while updating the tunnel: {e.Message}");
        }
    }

    public async Task<TunnelResponse> DeleteAsync(int tunnelId)
    {
        var existingTunnel = await _tunnelRepository.FindByIdAsync(tunnelId);
        
        // Validate Tunnel

        if (existingTunnel == null)
            return new TunnelResponse("Tunnel not found.");
        
        try
        {
            _tunnelRepository.Remove(existingTunnel);
            await _unitOfWork.CompleteAsync();

            return new TunnelResponse(existingTunnel);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new TunnelResponse($"An error occurred while deleting the tunnel: {e.Message}");
        }

    }
}