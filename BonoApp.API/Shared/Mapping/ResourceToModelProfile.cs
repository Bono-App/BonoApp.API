using AutoMapper;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Resources;
using BonoApp.API.User.Resources;

namespace BonoApp.API.Shared.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User.Domain.Models.User>();
            CreateMap<SaveBondResource, Bond>();
        }
    }
}