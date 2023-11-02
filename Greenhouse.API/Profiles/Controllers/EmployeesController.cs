using AutoMapper;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Services;
using Greenhouse.API.Profiles.Resources;
using Greenhouse.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Profiles.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<EmployeeResource>> GetAllAsync()
    {
        var employees = await _employeeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);

        return resources;

    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);

        var result = await _employeeService.SaveAsync(employee);

        if (!result.Success)
            return BadRequest(result.Message);

        var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);

        return Ok(employeeResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var employee = _mapper.Map<SaveEmployeeResource, Employee>(resource);

        var result = await _employeeService.UpdateAsync(id, employee);

        if (!result.Success)
            return BadRequest(result.Message);

        var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);

        return Ok(employeeResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _employeeService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var employeeResource = _mapper.Map<Employee, EmployeeResource>(result.Resource);

        return Ok(employeeResource);
    }

}