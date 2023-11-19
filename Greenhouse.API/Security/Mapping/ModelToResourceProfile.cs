using AutoMapper;
using Greenhouse.API.Security.Domain.Models;
using Greenhouse.API.Security.Domain.Services.Communication;
using Greenhouse.API.Security.Resources;

namespace Greenhouse.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}