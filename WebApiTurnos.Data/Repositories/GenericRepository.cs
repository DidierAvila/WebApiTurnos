using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTurnos.Data.DbContexts;

namespace WebApiTurnos.Data.Repositories
{
    internal abstract class GenericRepository<TEntity> where TEntity : Entity
    {
        protected readonly TurnoDbContext _context;

        public GenericRepository(TurnoDbContext context)
        {
            _context = context;
        }
    }
}
