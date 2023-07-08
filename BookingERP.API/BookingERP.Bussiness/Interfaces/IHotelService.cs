using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Hotel;
using BookingERP.Bussiness.Models.Manager;
using BookingERP.Bussiness.Models.Room;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IHotelService
    {
        Task AddAsync(HotelModel hotel);
        Task DeleteAsync(Guid id);
        Task<HotelModel> GetHotelByIdAsync(Guid id);
        Task UpdateAsync(HotelModel hotel);
        Task<IEnumerable<GuestResponseModel>> GetAllGuestsByHotelId(Guid id);
        Task<IEnumerable<HotelModel>> GetAllHotels();
    }
}
