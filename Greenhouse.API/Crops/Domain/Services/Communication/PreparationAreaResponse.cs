using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class PreparationAreaResponse : BaseResponse<PreparationArea>
{
    public PreparationAreaResponse(string message) : base(message)
    {
    }

    public PreparationAreaResponse(PreparationArea resource) : base(resource)
    {
    }
}