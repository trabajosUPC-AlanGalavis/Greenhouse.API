using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Profiles.Domain.Services.Communication;

public class EmployeeResponse : BaseResponse<Employee>
{
    public EmployeeResponse(string message) : base(message)
    {
    }

    public EmployeeResponse(Employee resource) : base(resource)
    {
    }
}