using AutoMapper;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Profiles.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCompanyResource, Company>();
        CreateMap<SaveEmployeeResource, Employee>();
    }
}