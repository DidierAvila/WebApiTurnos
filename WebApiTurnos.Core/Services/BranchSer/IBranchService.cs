using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Core.Services.BranchSer
{
    public interface IBranchService
    {
        Task<Branch> Create(Branch entity, CancellationToken cancellationToken);
        Task<Branch> Read(int id, CancellationToken cancellationToken);
        Task<Branch> Update(Branch entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<ICollection<Branch>> GetAll(CancellationToken cancellationToken);
    }
}