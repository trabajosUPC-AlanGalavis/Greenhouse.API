using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Resources;

namespace Greenhouse.API.Crops.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCropResource, Crop>();
        CreateMap<SaveFormulaResource, Formula>();
        CreateMap<SavePreparationAreaResource, PreparationArea>();
        CreateMap<SaveBunkerResource, Bunker>();
        CreateMap<SaveTunnelResource, Tunnel>();
        CreateMap<SaveGrowRoomRecordResource, GrowRoomRecord>();
    }
}