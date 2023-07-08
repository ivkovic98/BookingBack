using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingERP.Data.Repositories
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(BookingContext context) : base(context)
        {

        }

        public async Task<Reservation> GetReservationById(Guid id)
        {
            var reservation = await _context.Reservations
                        .Include(r => r.ReservationRooms)
                        .ThenInclude(rr => rr.Room)
                        .FirstOrDefaultAsync(r => r.Id == id);

            return reservation;
        }


        public async Task CreateReservation(Reservation reservation, List<ReservationRoom> reservationRooms)
        {
            //add reservation

            _context.Entry(reservation).State = EntityState.Detached;
            var newRes = _context.Reservations.AddAsync(reservation).Result.Entity;

            reservationRooms.ForEach(rr => rr.ReservationId = newRes.Id);
            await _context.ReservationRooms.AddRangeAsync(reservationRooms);

            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsForRoom(Guid roomId)
        {
            var reservations = await _context.Reservations.Where(r => r.ReservationRooms.Any(rr => rr.RoomId == roomId)).ToListAsync();

            return reservations;
        }
        public async Task<IEnumerable<Room>> GetAvailableRooms(DateTime startDate, DateTime endDate)
        {
            var availableRooms = await _context.Rooms
                                    .Where(room => !room.ReservationRooms
                                    .Any(rr => (startDate <= rr.Reservation.EndDate && endDate >= rr.Reservation.StartDate)
                                               || (startDate >= rr.Reservation.StartDate && startDate <= rr.Reservation.EndDate)
                                        ))
                                    .ToListAsync();

            return availableRooms;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByGuestId(Guid Id)
        {
            var reservations = await _context.Reservations
                        .Where(r => r.GuestId == Id)
                        .Include(h => h.Hotel)
                        .Include(g => g.Guest)
                        .Include(r => r.ReservationRooms)
                        .ThenInclude(rr => rr.Room)
                        .ToListAsync();

            return reservations;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByHotelId(Guid Id)
        {
            var reservations = await _context.Reservations
                        .Where(r => r.HotelId == Id)
                        .Include(h => h.Hotel)
                        .Include(g => g.Guest)
                        .Include(r => r.ReservationRooms)
                        .ThenInclude(rr => rr.Room)
                        .ToListAsync();

            return reservations;
        }
    }
}
