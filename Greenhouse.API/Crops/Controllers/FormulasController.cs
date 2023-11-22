using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services;
using Greenhouse.API.Crops.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Crops.Controllers;

[ApiController]
[Route("/api/v1/crops/{cropId}/[controller]")]
[Tags("Crops Formulas")]
public class FormulasController : ControllerBase
{
    private readonly IFormulaService _formulaService;
    private readonly IMapper _mapper;
    
    public FormulasController(IFormulaService formulaService, IMapper mapper)
    {
        _formulaService = formulaService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync(int cropId)
    {
        var formulas = await _formulaService.ListByCropIdAsync(cropId);
    
        var resources = _mapper.Map<IEnumerable<Formula>, IEnumerable<FormulaResource>>(formulas);
    
        return Ok(resources);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(int cropId,[FromBody] SaveFormulaResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var formula = _mapper.Map<SaveFormulaResource, Formula>(resource);
        
        formula.CropId = cropId;
    
        var result = await _formulaService.SaveAsync(formula);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var cropResource = _mapper.Map<Formula, FormulaResource>(result.Resource);
    
        return Ok(cropResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, int cropId, [FromBody] SaveFormulaResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
    
        var formula = _mapper.Map<SaveFormulaResource, Formula>(resource);
        
        formula.CropId = cropId;
    
        var result = await _formulaService.UpdateAsync(id, formula);
    
        if (!result.Success)
            return BadRequest(result.Message);
    
        var formulaResource = _mapper.Map<Formula, FormulaResource>(result.Resource);
    
        return Ok(formulaResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _formulaService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var formulaResource = _mapper.Map<Formula, FormulaResource>(result.Resource);

        return Ok(formulaResource);
    } 
}
