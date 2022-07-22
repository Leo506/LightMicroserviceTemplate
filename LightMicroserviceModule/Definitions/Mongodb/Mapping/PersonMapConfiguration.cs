using AutoMapper;
using LightMicroserviceModule.Definitions.Mongodb.Models;
using LightMicroserviceModule.Definitions.Mongodb.ViewModels;

namespace LightMicroserviceModule.Definitions.Mongodb.Mapping;

public class PersonMapConfiguration : Profile
{
    public PersonMapConfiguration()
    {
        CreateMap<PersonModel, PersonViewModel>()
            .ForMember(x => x.FirstName, o =>
                o.MapFrom(c => c.FirstName))
            .ForMember(x => x.LastName, o =>
                o.MapFrom(c => c.LastName));
    }
}