using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Login;
using BookingERP.Bussiness.Models.Manager;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponseModel> Login(LoginModel model);
        Task RegisterGuest(GuestRegisterModel model);
        Task RegisterManager(ManagerRegisterModel model);
    }
}
