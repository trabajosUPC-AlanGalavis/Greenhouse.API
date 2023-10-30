using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class TunnelResponse : BaseResponse<Tunnel>
{
    public TunnelResponse(string message) : base(message)
    {
    }

    public TunnelResponse(Tunnel resource) : base(resource)
    {
    }
}