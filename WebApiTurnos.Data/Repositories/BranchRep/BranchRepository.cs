using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiTurnos.Data.DbContexts;
using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Data.Repositories.BranchRep
{
    public class BranchRepository : IBranchRepository
    {
        protected readonly TurnoDbContext _context;

        public BranchRepository(TurnoDbContext context)
        {
            _context = context;
        }

        public async Task<Branch> Create(Branch entity, CancellationToken cancellationToken)
        {
            var result = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<Branch> Read(int id, CancellationToken cancellationToken)
        {
            return await _context.Branches.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Branch> Update(Branch entity, CancellationToken cancellationToken)
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
                _context.Branches.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ICollection<Branch>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Branches.ToListAsync(cancellationToken);
        }
    }
}
