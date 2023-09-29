using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTurnos.Data.DbContexts;
using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Data.Repositories.SecurityRep
{
    public class SecurityRepository
    {
        protected readonly TurnoDbContext _context;

        public SecurityRepository(TurnoDbContext context)
        {
            _context = context;
        }

        public async Task<User> Login(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
