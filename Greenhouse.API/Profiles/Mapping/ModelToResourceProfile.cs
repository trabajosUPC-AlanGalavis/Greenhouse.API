using AutoMapper;
using Greenhouse.API.Profiles.Domain.Models;
using Greenhouse.API.Profiles.Resources;

namespace Greenhouse.API.Profiles.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Company, CompanyResource>();
        CreateMap<Employee, EmployeeResource>();
    }
}