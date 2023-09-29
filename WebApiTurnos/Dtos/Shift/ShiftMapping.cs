using AutoMapper;
using WebApiTurnos.Dtos.Shift;

namespace WebApiTurnos.Dtos.Shift
{
    public class ShiftMapping : Profile
    {
        public ShiftMapping()
        {
            CreateMap<ShiftBase, Data.Models.Shift>();
            CreateMap<Data.Models.Shift, ShiftGeneric>();
        }
    }
}
