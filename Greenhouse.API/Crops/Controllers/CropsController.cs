using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CropsController : ControllerBase
{
    private readonly ICropService _cropService;
    private readonly IMapper _mapper;

    public CropsController(ICropService cropService, IMapper mapper)
    {
        _cropService = cropService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CropResource>> GetAllAsync()
    {
        var crops = await _cropService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Crop>, IEnumerable<CropResource>>(crops);

        return resources;
    }
    
    [HttpGet("{companyId}")]
    public async Task<IEnumerable<CropResource>> GetAllByCompanyIdAsync(int companyId)
    {
        var crops = await _cropService.ListByCompanyIdAsync(companyId);
        var resources = _mapper.Map<IEnumerable<Crop>, IEnumerable<CropResource>>(crops);

        return resources;
    }

    [HttpPost("{companyId}")]
    public async Task<IActionResult> PostAsync(int companyId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var resource = new SaveCropResource();
        
        var crop = _mapper.Map<SaveCropResource, Crop>(resource);

        crop.CompanyId = companyId;
        var result = await _cropService.SaveAsync(crop);

        if (!result.Success)
            return BadRequest(result.Message);

        var cropResource = _mapper.Map<Crop, CropResource>(result.Resource);

        return Ok(cropResource);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        //var resource = new SaveCropResource();

        //var crop = _mapper.Map<SaveCropResource, Crop>(resource);

        var crop = await _cropService.GetByIdAsync(id);
        var result = await _cropService.UpdateAsync(id, crop);

        if (!result.Success)
            return BadRequest(result.Message);

        var cropResource = _mapper.Map<Crop, CropResource>(result.Resource);

        return Ok(cropResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _cropService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var cropResource = _mapper.Map<Crop, CropResource>(result.Resource);

        return Ok(cropResource);
    }

}