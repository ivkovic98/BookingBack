using BookingERP.Data.Entities;

namespace BookingERP.Data.Interfaces
{
    public interface IReservationRepository :IGenericRepository<Reservation>
    {
        Task<IEnumerable<Reservation>> GetAllReservationsForRoom(Guid roomId);
        Task<IEnumerable<Room>> GetAvailableRooms(DateTime startDate, DateTime endDate);
        Task CreateReservation(Reservation reservation, List<ReservationRoom> reservationRooms);
        Task<Reservation> GetReservationById(Guid id);
        Task<IEnumerable<Reservation>> GetReservationsByGuestId(Guid Id);
        Task<IEnumerable<Reservation>> GetReservationsByHotelId(Guid Id);

    }
}
