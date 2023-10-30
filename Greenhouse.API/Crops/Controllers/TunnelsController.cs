using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
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
    public async Task<IEnumerable<TunnelResource>> GetAllAsync()
    {
        var tunnels = await _tunnelService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Tunnel>, IEnumerable<TunnelResource>>(tunnels);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveTunnelResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var tunnel = _mapper.Map<SaveTunnelResource, Tunnel>(resource);
    
        var result = await _tunnelService.SaveAsync(tunnel);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var cropResource = _mapper.Map<Tunnel, TunnelResource>(result.Resource);
    
        return Ok(cropResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTunnelResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var tunnel = _mapper.Map<SaveTunnelResource, Tunnel>(resource);
    
        var result = await _tunnelService.UpdateAsync(id, tunnel);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var tunnelResource = _mapper.Map<Tunnel, TunnelResource>(result.Resource);
    
        return Ok(tunnelResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _tunnelService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var tunnelResource = _mapper.Map<Tunnel, TunnelResource>(result.Resource);

        return Ok(tunnelResource);
    } 
}