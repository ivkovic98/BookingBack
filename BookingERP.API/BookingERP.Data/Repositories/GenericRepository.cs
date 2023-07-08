using BookingERP.Data.Context;
using BookingERP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingERP.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly BookingContext _context ;
        private readonly DbSet<T> _table;

        public GenericRepository(BookingContext context)
        {
            this._context = context;
            _table = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<T> GetByIdLazy(Guid Id)
        {
            return await _table.FindAsync(Id);
        }
        public async Task DeleteAsync(Guid id)
        {
             var entity = await _table.FindAsync(id);
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
             _table.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
} 
