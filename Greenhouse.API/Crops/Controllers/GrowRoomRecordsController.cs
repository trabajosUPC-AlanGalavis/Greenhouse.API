using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/crops/{cropId}/[controller]")]
[Tags("Crops Grow Room Records")]
public class GrowRoomRecordsController : ControllerBase
{
    private readonly IGrowRoomRecordService _growRoomRecordService;
    private readonly IMapper _mapper;
    
    public GrowRoomRecordsController(IGrowRoomRecordService growRoomRecordService, IMapper mapper)
    {
        _growRoomRecordService = growRoomRecordService;
        _mapper = mapper;
    }
    
    [HttpGet("{cropPhase}")]
    public async Task<IEnumerable<GrowRoomRecordResource>> GetAllAsync(int cropId, string cropPhase)
    {
        var growRoomRecords = await _growRoomRecordService.ListByCropIdAndProcessTypeAsync(cropId, cropPhase);
        var resources = _mapper.Map<IEnumerable<GrowRoomRecord>, IEnumerable<GrowRoomRecordResource>>(growRoomRecords);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(int cropId,[FromBody] SaveGrowRoomRecordResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var growRoomRecord = _mapper.Map<SaveGrowRoomRecordResource, GrowRoomRecord>(resource);
        
        growRoomRecord.CropId = cropId;
    
        var result = await _growRoomRecordService.SaveAsync(growRoomRecord);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var cropResource = _mapper.Map<GrowRoomRecord, GrowRoomRecordResource>(result.Resource);
    
        return Ok(cropResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, int cropId, [FromBody] SaveGrowRoomRecordResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var growRoomRecord = _mapper.Map<SaveGrowRoomRecordResource, GrowRoomRecord>(resource);
        
        growRoomRecord.CropId = cropId;
    
        var result = await _growRoomRecordService.UpdateAsync(id, growRoomRecord);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var growRoomRecordResource = _mapper.Map<GrowRoomRecord, GrowRoomRecordResource>(result.Resource);
    
        return Ok(growRoomRecordResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, int cropId)
    {
        var result = await _growRoomRecordService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var growRoomRecordResource = _mapper.Map<GrowRoomRecord, GrowRoomRecordResource>(result.Resource);

        return Ok(growRoomRecordResource);
    } 
}
