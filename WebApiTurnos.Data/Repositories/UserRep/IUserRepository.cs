using WebApiTurnos.Data.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WebApiTurnos.Data.Repositories.UserRep
{
    public interface IUserRepository
    {
        Task<User> Create(User entity, CancellationToken cancellationToken);
        Task<User> Read(int id, CancellationToken cancellationToken);
        Task<User> Update(User entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<ICollection<User>> GetAll(CancellationToken cancellationToken);
        Task<User> Login(User userLogin, CancellationToken cancellationToken);
    }
}