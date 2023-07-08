using BookingERP.Data.Entities;

namespace BookingERP.Data.Interfaces
{
    public interface IGuestRepository : IGenericRepository<Guest>
    {
        Task<Guest> GetGuestByUserId(Guid guid);
        Task<Guest> GetGuestById(Guid id);

    }
}
