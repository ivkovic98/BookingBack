using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingERP.Data.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(BookingContext context) : base(context)
        {
        }

        public async Task<Hotel> GetHotelById(Guid guid)
        {
            var hotel = await _context.Hotels
                                    .Where(h => h.Id == guid)
                                    .FirstOrDefaultAsync();
            return hotel;
        }

        public async Task<IEnumerable<Room>> GetAllHotelRooms(Guid id)
        {
            var hotel = await _context.Hotels.Where(h => h.Id == id).Include(h => h.RoomList).FirstOrDefaultAsync();
            return hotel.RoomList;
        }

        public async Task<IEnumerable<Guest>> GetAllGuestsByHotelId(Guid id)
        {
            var hotelReservations = await _context.Reservations
                .Where(h => h.HotelId == id)
                .Include(g => g.Guest)
                .ToListAsync();

            var guests = hotelReservations.Select(g => g.Guest);

            return guests;
        }
    }
}
