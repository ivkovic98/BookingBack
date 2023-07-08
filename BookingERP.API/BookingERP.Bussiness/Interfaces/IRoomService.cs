using BookingERP.Bussiness.Models.Room;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IRoomService
    {
        Task AddAsync(RoomModel room);
        Task<RoomModel> GetRoomById(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(RoomModel room);
        Task<IEnumerable<RoomModel>> GetRoomsByHotelId(Guid hotelId);

    }
}
