using BookingERP.Data.Entities;

namespace BookingERP.Data.Interfaces
{
    public interface IRoomRepository :IGenericRepository<Room>
    {
        Task<Room> GetRoomById(Guid id);

        Task<IEnumerable<Room>> GetAllRoomsByHotelId(Guid id);
    }
}
