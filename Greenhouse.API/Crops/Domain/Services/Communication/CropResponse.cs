using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Shared.Domain.Services.Communication;

namespace Greenhouse.API.Crops.Domain.Services.Communication;

public class CropResponse : BaseResponse<Crop>
{
    public CropResponse(string message) : base(message)
    {
    }

    public CropResponse(Crop resource) : base(resource)
    {
    }
}