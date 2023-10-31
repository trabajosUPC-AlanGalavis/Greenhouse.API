using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class GrowRoomRecordResponse: BaseResponse<GrowRoomRecord>
{
    public GrowRoomRecordResponse(string message) : base(message)
    {
    }

    public GrowRoomRecordResponse(GrowRoomRecord resource) : base(resource)
    {
    }
}