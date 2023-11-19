using AutoMapper;
using Greenhouse.API.Crops.Domain.Models;
using Greenhouse.API.Crops.Domain.Services.Communication;
using Greenhouse.API.Crops.Resources;

namespace Greenhouse.API.Crops.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Crop, CropResource>();
        CreateMap<Formula, FormulaResource>();
        CreateMap<PreparationArea, PreparationAreaResource>();
        CreateMap<Bunker, BunkerResource>();
        CreateMap<Tunnel, TunnelResource>();
        CreateMap<GrowRoomRecord, GrowRoomRecordResource>();
    }
}