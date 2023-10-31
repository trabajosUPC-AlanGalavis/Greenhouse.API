using AutoMapper;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Domain.Services;
using Greenhouse.API.Profiles.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Profiles.Controllers;

[ApiController]
[Route("/api/v1/companies/{companyId}/employees")]
public class CompanyEmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public CompanyEmployeesController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<EmployeeResource>> GetAllByCompanyIdAsync(int companyId)
    {
        var employees = await _employeeService.ListByCompanyIdAsync(companyId);

        var resources = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResource>>(employees);

        return resources;
    }
}