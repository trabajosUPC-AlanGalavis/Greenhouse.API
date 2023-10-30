using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class PhaseResponse : BaseResponse<Phase>
{
    public PhaseResponse(string message) : base(message)
    {
    }

    public PhaseResponse(Phase resource) : base(resource)
    {
    }
}