using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingERP.Data.Repositories
{
    public class ManagerRepository : GenericRepository<Manager>, IManagerRepostirory
    {
        public ManagerRepository(BookingContext context) : base(context)
        {
        }

        public async Task<Manager> GetManagerByUserIdAsync(Guid guid)
        {
            var manager = await _context.Managers
                                       .Where(x => x.User.Id == guid)
                                       .FirstOrDefaultAsync();
            return manager;
        }

        public async Task<Manager> GetMaganerByIdAsync(Guid guid)
        {
            var manager = await _context.Managers
                                        .Where(x=>x.Id == guid)
                                        .Include(u => u.User)
                                        .FirstOrDefaultAsync();
            return manager;
        }

        public async Task<IEnumerable<Manager>> GetManagersByHotelId(Guid hotelId)
        {
            var managers = await _context.Managers.Where(x => x.HotelId == hotelId)
                                                    .Include(u => u.User)
                                                    .ToListAsync();
                return managers;
        }
        
    }
}
