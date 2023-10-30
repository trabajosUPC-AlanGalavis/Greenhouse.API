using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Repositories;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Shared.Domain.Repositories;

namespace Greenhouse.API.Crops.Services;

public class PhaseService : IPhaseService
{
    private readonly IPhaseRepository _phaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PhaseService(IPhaseRepository phaseRepository, IUnitOfWork unitOfWork)
    {
        _phaseRepository = phaseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Phase>> ListAsync()
    {
        return await _phaseRepository.ListAsync();
    }

    public async Task<PhaseResponse> SaveAsync(Phase phase)
    {
        try
        {
            await _phaseRepository.AddAsync(phase);
            await _unitOfWork.CompleteAsync();
            return new PhaseResponse(phase);
        }
        catch (Exception e)
        {
            return new PhaseResponse($"An error occurred while saving the phase: {e.Message}");
        }
    }

    public async Task<PhaseResponse> UpdateAsync(int id, Phase phase)
    {
        var existingPhase = await _phaseRepository.FindByIdAsync(id);

        if (existingPhase == null)
            return new PhaseResponse("Phase not found.");

        existingPhase.Name = phase.Name;

        try
        {
            _phaseRepository.Update(existingPhase);
            await _unitOfWork.CompleteAsync();

            return new PhaseResponse(existingPhase);
        }
        catch (Exception e)
        {
            return new PhaseResponse($"An error occurred while updating the phase: {e.Message}");
        }
    }

    public async Task<PhaseResponse> DeleteAsync(int id)
    {
        var existingPhase = await _phaseRepository.FindByIdAsync(id);

        if (existingPhase == null)
            return new PhaseResponse("Phase not found.");

        try
        {
            _phaseRepository.Remove(existingPhase);
            await _unitOfWork.CompleteAsync();

            return new PhaseResponse(existingPhase);
        }
        catch (Exception e)
        {
            // Error Handling
            return new PhaseResponse($"An error occurred while deleting the phase: {e.Message}");
        }
    }
}