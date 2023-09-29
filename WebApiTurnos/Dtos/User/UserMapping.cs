using AutoMapper;

namespace WebApiTurnos.Dtos.User
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserBase, Data.Models.User>();
            CreateMap<Data.Models.User, UserGeneric>();
        }
    }
}
