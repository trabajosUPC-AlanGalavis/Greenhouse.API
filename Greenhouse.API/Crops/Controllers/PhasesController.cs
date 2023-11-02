using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class PhasesController : ControllerBase
{
    private readonly IPhaseService _phaseService;
    private readonly IMapper _mapper;
    

    public PhasesController(IPhaseService phaseService, IMapper mapper)
    {
        _phaseService = phaseService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PhaseResource>> GetAllAsync()
    {
        var phases = await _phaseService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Phase>, IEnumerable<PhaseResource>>(phases);

        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePhaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var phase = _mapper.Map<SavePhaseResource, Phase>(resource);

        var result = await _phaseService.SaveAsync(phase);

        if (!result.Success)
            return BadRequest(result.Message);

        var phaseResource = _mapper.Map<Phase, PhaseResource>(result.Resource);

        return Ok(phaseResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePhaseResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var phase = _mapper.Map<SavePhaseResource, Phase>(resource);
        var result = await _phaseService.UpdateAsync(id, phase);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var phaseResource = _mapper.Map<Phase, PhaseResource>(result.Resource);

        return Ok(phaseResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _phaseService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var phaseResource = _mapper.Map<Phase, PhaseResource>(result.Resource);

        return Ok(phaseResource);
    }
}