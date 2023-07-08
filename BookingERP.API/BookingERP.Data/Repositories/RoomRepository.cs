using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingERP.Data.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingContext context) : base(context)
        {

        }

        public async Task<Room> GetRoomById(Guid guid)
        {
            var room = await _context.Rooms
                                    .Where(x => x.Id == guid)
                                    .Include(h => h.Hotel)
                                    .FirstOrDefaultAsync();
            return room;
        }


        public async Task<IEnumerable<Room>> GetAllRoomsByHotelId(Guid id)
        {
            var rooms = await _context.Rooms
                                .Where(x => x.HotelId == id)
                                .ToListAsync();
            return rooms;
        }
    }
}
