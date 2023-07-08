using BookingERP.Data.Entities;

namespace BookingERP.Data.Interfaces
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<Hotel> GetHotelById(Guid id);
        Task<IEnumerable<Room>> GetAllHotelRooms(Guid id);
        Task<IEnumerable<Guest>>  GetAllGuestsByHotelId(Guid id);
    }
}
