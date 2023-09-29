using Microsoft.EntityFrameworkCore;
using WebApiTurnos.Data.DbContexts;
using WebApiTurnos.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTurnos.Data.Repositories.UserRep
{
    public class UserRepository : IUserRepository
    {
        protected readonly TurnoDbContext _context;

        public UserRepository(TurnoDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User entity, CancellationToken cancellationToken)
        {
            var result = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<User> Read(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User> Update(User entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await Read(id, cancellationToken);
            if (entity != null)
            {
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ICollection<User>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> Login(User userLogin, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(b => b.Email.ToLower() == userLogin.Email.ToLower() && b.Password == userLogin.Password).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
