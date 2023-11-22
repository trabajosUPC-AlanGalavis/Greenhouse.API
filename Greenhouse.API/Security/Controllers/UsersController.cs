using AutoMapper;
using Greenhouse.API.Security.Domain.Models;
using Greenhouse.API.Security.Domain.Services;
using Greenhouse.API.Security.Domain.Services.Communication;
using Greenhouse.API.Security.Resources;
using Greenhouse.API.Security.Authorization.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse.API.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        await _userService.RegisterAsync(request);
        return Ok(new {message = "Registration successful"});
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.ListAsync();
        var response = _mapper.Map<IEnumerable<User>, 
            IEnumerable<UserResource>>(users);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _userService.UpdateAsync(id, request);
        return Ok(new { message = "User updated successfully"});
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully"});
    }
}