using BookingERP.Bussiness.Models.Guest;
using BookingERP.Data.Entities;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IGuestService
    {
        Task CreateGuestAsync(ApplicationUser user, GuestRegisterModel model);
        Task<GuestResponseModel> GetGuestById(Guid id);
    }
}
