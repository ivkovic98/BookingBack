using BookingERP.Bussiness.Models.Manager;
using BookingERP.Data.Entities;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IManagerService
    {
        Task CreateManagerAsync(ApplicationUser user, ManagerRegisterModel model);
        Task<ManagerResponseModel> GetMaganerByIdAsync(Guid id);
        Task<IEnumerable<ManagerResponseModel>> GetAllManagersByHotelId(Guid hotelId);

    }
}
