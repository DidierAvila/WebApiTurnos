using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Core.Services.ShiftSer
{
    public interface IShiftService
    {
        Task<Shift> Create(Shift entity, CancellationToken cancellationToken);
        Task<Shift> Read(int id, CancellationToken cancellationToken);
        Task<Shift> Update(Shift entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<ICollection<Shift>> Search(Shift search, CancellationToken cancellationToken);
        Task<Shift> ActivateShift(int shiftId, CancellationToken cancellationToken);
        Task<Shift> CancelShift(int id, CancellationToken cancellationtoken);
    }
}