using BookingERP.Data.Entities;

namespace BookingERP.Data.Interfaces
{
    public interface IManagerRepostirory : IGenericRepository<Manager>
    {
        Task<Manager> GetManagerByUserIdAsync(Guid guid);
        Task<Manager> GetMaganerByIdAsync(Guid id);
        Task<IEnumerable<Manager>> GetManagersByHotelId(Guid hotelId);
    }
}
