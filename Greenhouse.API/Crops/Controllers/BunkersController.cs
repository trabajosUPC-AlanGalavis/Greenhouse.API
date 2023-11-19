using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class BunkersController : ControllerBase
{
    private readonly IBunkerService _bunkerService;
    private readonly IMapper _mapper;
    
    public BunkersController(IBunkerService bunkerService, IMapper mapper)
    {
        _bunkerService = bunkerService;
        _mapper = mapper;
    }
    
    [HttpGet("{cropId}")]
    public async Task<IEnumerable<BunkerResource>> GetAllAsync(int cropId)
    {
        var bunkers = await _bunkerService.ListByCropIdAsync(cropId);
        var resources = _mapper.Map<IEnumerable<Bunker>, IEnumerable<BunkerResource>>(bunkers);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveBunkerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var bunker = _mapper.Map<SaveBunkerResource, Bunker>(resource);
    
        var result = await _bunkerService.SaveAsync(bunker);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var cropResource = _mapper.Map<Bunker, BunkerResource>(result.Resource);
    
        return Ok(cropResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBunkerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var bunker = _mapper.Map<SaveBunkerResource, Bunker>(resource);
    
        var result = await _bunkerService.UpdateAsync(id, bunker);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var bunkerResource = _mapper.Map<Bunker, BunkerResource>(result.Resource);
    
        return Ok(bunkerResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _bunkerService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var bunkerResource = _mapper.Map<Bunker, BunkerResource>(result.Resource);

        return Ok(bunkerResource);
    } 
}