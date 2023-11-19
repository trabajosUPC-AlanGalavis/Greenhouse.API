using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/crops/{cropId}/[controller]")]
[Tags("Crops Tunnel")]
public class TunnelsController : ControllerBase
{
    private readonly ITunnelService _tunnelService;
    private readonly IMapper _mapper;
    
    public TunnelsController(ITunnelService tunnelService, IMapper mapper)
    {
        _tunnelService = tunnelService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<TunnelResource>> GetAllAsync(int cropId)
    {
        var tunnels = await _tunnelService.ListByCropIdAsync(cropId);
        var resources = _mapper.Map<IEnumerable<Tunnel>, IEnumerable<TunnelResource>>(tunnels);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(int cropId,[FromBody] SaveTunnelResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var tunnel = _mapper.Map<SaveTunnelResource, Tunnel>(resource);
        
        tunnel.CropId = cropId;
    
        var result = await _tunnelService.SaveAsync(tunnel);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var cropResource = _mapper.Map<Tunnel, TunnelResource>(result.Resource);
    
        return Ok(cropResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, int cropId,[FromBody] SaveTunnelResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var tunnel = _mapper.Map<SaveTunnelResource, Tunnel>(resource);
        
        tunnel.CropId = cropId;
    
        var result = await _tunnelService.UpdateAsync(id, tunnel);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var tunnelResource = _mapper.Map<Tunnel, TunnelResource>(result.Resource);
    
        return Ok(tunnelResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, int cropId)
    {
        var result = await _tunnelService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var tunnelResource = _mapper.Map<Tunnel, TunnelResource>(result.Resource);

        return Ok(tunnelResource);
    } 
}