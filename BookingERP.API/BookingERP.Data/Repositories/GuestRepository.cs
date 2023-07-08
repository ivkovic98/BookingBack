using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingERP.Data.Repositories
{
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository
    {

        public GuestRepository(BookingContext context) : base(context)
        {

        }

        public async Task<Guest> GetGuestByUserId(Guid guid)
        {
            var guest = await _context.Guests
                                       .Where(x => x.User.Id == guid)
                                       .FirstOrDefaultAsync();
            return guest;
        }

        public async Task<Guest> GetGuestById(Guid guid)
        {
            var guest = await _context.Guests
                                    .Where(x=>x.Id==guid)
                                    .Include(u => u.User)
                                    .FirstOrDefaultAsync();
            return guest;
        }

    }
}
