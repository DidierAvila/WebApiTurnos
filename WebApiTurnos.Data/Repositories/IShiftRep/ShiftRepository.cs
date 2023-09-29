using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTurnos.Data.DbContexts;
using WebApiTurnos.Data.Models;

namespace WebApiTurnos.Data.Repositories.IShiftRep
{
    public class ShiftRepository : IShiftRepository
    {
        protected readonly TurnoDbContext _context;

        public ShiftRepository(TurnoDbContext context)
        {
            _context = context;
        }

        public async Task<Shift> Create(Shift entity, CancellationToken cancellationToken)
        {
            var result = await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<Shift> Read(int id, CancellationToken cancellationToken)
        {
            return await _context.Shifts.Where(b => b.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Shift> Update(Shift entity, CancellationToken cancellationToken)
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
                _context.Shifts.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<ICollection<Shift>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Shifts.ToListAsync(cancellationToken);
        }

        public async Task<ICollection<Shift>> Search(Shift search, CancellationToken cancellationToken)
        {
            var query = from resultShifts in _context.Shifts select resultShifts;
            if (search.Id != 0)
            {
                query = query.Where(x => x.Id == search.Id);
            }
            if (search.UserId != null && search.UserId != 0)
            {
                query = query.Where(x => x.UserId == search.UserId);
            }
            if (search.BranchId != 0)
            {
                query = query.Where(x => x.BranchId == search.BranchId);
            }
            if (search.ShiftDate != default)
            {
                query = query.Where(x => x.ShiftDate.Date == search.ShiftDate.Date);
            }
            if (search.Status != 0)
            {
                query = query.Where(x => x.Status == search.Status);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
