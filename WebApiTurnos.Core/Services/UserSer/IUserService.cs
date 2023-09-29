using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Core.Services.UserSer
{
    public interface IUserService
    {
        Task<User> Create(User createRequest, CancellationToken cancellationToken);
        Task<User> Read(int id, CancellationToken cancellationToken);
        Task<User> Update(User entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<ICollection<User>> GetAll(CancellationToken cancellationToken);
        Task<string> Login(User userLogin, CancellationToken cancellationToken);
    }
}