using WebApiTurnos.Data.Models;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace WebApiTurnos.Data.Repositories.IShiftRep
{
    public interface IShiftRepository
    {
        Task<Shift> Create(Shift entity, CancellationToken cancellationToken);
        Task<Shift> Read(int id, CancellationToken cancellationToken);
        Task<Shift> Update(Shift entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<ICollection<Shift>> Search(Shift search, CancellationToken cancellationToken);
    }
}