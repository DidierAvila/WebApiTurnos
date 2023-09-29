using AutoMapper;
using WebApiTurnos.Dtos.User;

namespace WebApiTurnos.Dtos.Branch
{
    public class BranchMapping : Profile
    {
        public BranchMapping()
        {
            CreateMap<BranchBase, Data.Models.Branch>();
            CreateMap<Data.Models.Branch, BranchGeneric>();
        }
    }
}
