using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/phases/{phaseId}/crops")]
public class CropPhasesController : ControllerBase
{
    private readonly ICropService _cropService;
    private readonly IMapper _mapper;

    public CropPhasesController(ICropService cropService, IMapper mapper)
    {
        _cropService = cropService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CropResource>> GetAllByPhaseIdAsync(int phaseId)
    {
        var crops = await _cropService.ListByPhaseIdAsync(phaseId);

        var resources = _mapper.Map<IEnumerable<Crop>, IEnumerable<CropResource>>(crops);

        return resources;
    }
}