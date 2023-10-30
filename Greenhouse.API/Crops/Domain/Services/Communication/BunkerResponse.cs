using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class BunkerResponse : BaseResponse<Bunker>
{
    public BunkerResponse(string message) : base(message)
    {
    }

    public BunkerResponse(Bunker resource) : base(resource)
    {
    }
}