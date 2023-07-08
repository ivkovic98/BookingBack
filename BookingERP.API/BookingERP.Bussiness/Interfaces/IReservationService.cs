using BookingERP.Bussiness.Models.Reservation;
using BookingERP.Bussiness.Models.Room;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IReservationService
    {
        Task AddAsync(ReservationModel model);
        Task DeleteAsync(Guid Id);
        Task<IEnumerable<RoomModel>> CheckAvailableRooms(DateTime startDate, DateTime endDate);
        Task<ReservationResponseModel> GetReservationById(Guid Id);
        Task<IEnumerable<ReservationResponseModel>> GetReservationsByGuestId(Guid Id);
        Task<IEnumerable<ReservationResponseModel>> GetReservationsByHotelId(Guid Id);


    }
}
