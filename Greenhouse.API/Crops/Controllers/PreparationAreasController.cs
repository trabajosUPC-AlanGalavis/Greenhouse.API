using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/crops/{cropId}/[controller]")]
[Tags("Crops Preparation Areas")]
public class PreparationAreasController : ControllerBase
{
    private readonly IPreparationAreaService _preparationAreaService;
    private readonly IMapper _mapper;
    
    public PreparationAreasController(IPreparationAreaService preparationAreaService, IMapper mapper)
    {
        _preparationAreaService = preparationAreaService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<PreparationAreaResource>> GetAllAsync(int cropId)
    {
        var preparationAreas = await _preparationAreaService.ListByCropIdAsync(cropId);
        var resources = _mapper.Map<IEnumerable<PreparationArea>, IEnumerable<PreparationAreaResource>>(preparationAreas);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(int cropId,[FromBody] SavePreparationAreaResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var preparationArea = _mapper.Map<SavePreparationAreaResource, PreparationArea>(resource);
        
        preparationArea.CropId = cropId;
    
        var result = await _preparationAreaService.SaveAsync(preparationArea);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var cropResource = _mapper.Map<PreparationArea, PreparationAreaResource>(result.Resource);
    
        return Ok(cropResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, int cropId,[FromBody] SavePreparationAreaResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var preparationArea = _mapper.Map<SavePreparationAreaResource, PreparationArea>(resource);
        
        preparationArea.CropId = cropId;
    
        var result = await _preparationAreaService.UpdateAsync(id, preparationArea);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var preparationAreaResource = _mapper.Map<PreparationArea, PreparationAreaResource>(result.Resource);
    
        return Ok(preparationAreaResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, int cropId)
    {
        var result = await _preparationAreaService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var preparationAreaResource = _mapper.Map<PreparationArea, PreparationAreaResource>(result.Resource);

        return Ok(preparationAreaResource);
    } 
}

