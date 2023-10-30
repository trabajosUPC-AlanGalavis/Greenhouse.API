using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Profiles.Domain.Services.Communication;

public class CompanyResponse : BaseResponse<Company>
{
    public CompanyResponse(string message) : base(message)
    {
    }

    public CompanyResponse(Company resource) : base(resource)
    {
    }
}