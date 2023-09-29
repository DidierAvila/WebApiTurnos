using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTurnos.Data.Repositories.IShiftRep;

namespace WebApiTurnos.Core.DomainShift
{
    internal class BusinesLogicShift
    {
        private readonly IMapper _mapper;
        private readonly IShiftRepository _repository;

        public BusinesLogicShift(IMapper mapper, IShiftRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void UpdateStatusExpire()
        {
             
        }
    }
}
