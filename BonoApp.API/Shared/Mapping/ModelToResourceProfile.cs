using AutoMapper;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Resources;
using BonoApp.API.User.Resources;

namespace BonoApp.API.Shared.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User.Domain.Models.User, UserResource>();
            CreateMap<Bond, BondResource>();
        }
    }
}