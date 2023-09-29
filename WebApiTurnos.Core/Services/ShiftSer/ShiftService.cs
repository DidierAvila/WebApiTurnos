using AutoMapper;
using System.Threading;
using WebApiTurnos.Core.Enum;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Data.Repositories.IShiftRep;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApiTurnos.Core.Services.ShiftSer
{
    public class ShiftService : IShiftService
    {
        private readonly IMapper _mapper;
        private readonly IShiftRepository _repository;

        public ShiftService(IMapper mapper , IShiftRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Shift> Create(Shift createRequest, CancellationToken cancellationToken)
        {
            Shift filter = new Shift { UserId = createRequest.UserId, Status = (int)EnumStatus.Agendado };
            ICollection<Shift> validationAgenda = await Search(filter, cancellationToken);
            filter = new Shift { UserId = createRequest.UserId, Status = (int)EnumStatus.Activo };
            ICollection<Shift> validationActivo = await Search(filter, cancellationToken);
            if (validationActivo != null && validationActivo.Count > 0 || validationAgenda != null && validationAgenda.Count > 0)
            {
                filter.Error = "El usuario ya tiene una cita agendada!";
                return filter;
            }
            return await _repository.Create(createRequest, cancellationToken);
        }

        public async Task<Shift> Read(int id, CancellationToken cancellationtoken)
        {
            return await _repository.Read(id, cancellationtoken);
        }

        public async Task<Shift> Update(Shift updateRequest, CancellationToken cancellationToken)
        {
            return await _repository.Update(updateRequest, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
        }

        public async Task<ICollection<Shift>> Search(Shift search, CancellationToken cancellationToken)
        {
            return await _repository.Search(search, cancellationToken);
        }

        public async Task<Shift> ActivateShift(int shiftId, CancellationToken cancellationToken)
        {
            Shift currentShift = await Read(shiftId, cancellationToken);
            if (!(currentShift.Status != (int)EnumStatus.Activo && currentShift.Status != (int)EnumStatus.Agendado))
            {
                if (currentShift.ShiftDate.AddMinutes(15) <= DateTime.Now.AddMinutes(15) && DateTime.Now >= currentShift.ShiftDate)
                {
                    currentShift.Status = (int)EnumStatus.Activo;
                }
                return await _repository.Update(currentShift, cancellationToken);
            }
            currentShift.Error = "No se puede activar el turno, estado:"+ currentShift.Status;
            return currentShift;
        }

        public async Task<Shift> CancelShift(int id, CancellationToken cancellationtoken)
        {
            var currentShift = await _repository.Read(id, cancellationtoken);
            currentShift.UserId = 0;
            currentShift.Status = (int)EnumStatus.Disponible;

            await Update(currentShift, cancellationtoken);
            return await _repository.Read(id, cancellationtoken);
        }
    }
}
