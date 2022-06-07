using AutoMapper;
using BonoApp.API.User.Resources;

namespace BonoApp.API.Shared.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User.Domain.Models.User, UserResource>();
        }
    }
}