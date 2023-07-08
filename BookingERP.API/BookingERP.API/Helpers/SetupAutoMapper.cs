using AutoMapper;
using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Hotel;
using BookingERP.Bussiness.Models.Manager;
using BookingERP.Bussiness.Models.Reservation;
using BookingERP.Bussiness.Models.Room;
using BookingERP.Data.Entities;

namespace BookingERP.API.Helpers

{
    public class SetupAutoMapper : Profile
    {
        public SetupAutoMapper()
        {

            #region User Mapping
            CreateMap<GuestRegisterModel, ApplicationUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ManagerRegisterModel, ApplicationUser>().ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));


            CreateMap<ApplicationUser, GuestResponseModel>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, ManagerResponseModel>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            #endregion

            #region Guest Mapping
            CreateMap<GuestRegisterModel, Guest>().ReverseMap();
            CreateMap<GuestResponseModel, Guest>().ReverseMap();

            CreateMap<Guest, GuestResponseModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber));
            #endregion

            #region Manager Mapping
            CreateMap<ManagerRegisterModel, Manager>().ReverseMap();
            CreateMap<ManagerResponseModel, Manager>().ReverseMap();
            
            CreateMap<Manager, ManagerResponseModel>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.PhoneNumber)); 
            #endregion

            #region Hotel Mapping
            CreateMap<HotelModel, Hotel>().ReverseMap();
            #endregion

            #region Room Mapping
            CreateMap<RoomModel, Room>().ReverseMap();

            CreateMap<ReservationRoom, RoomModel>();
            #endregion

            #region Reserevation Mapping
            CreateMap<ReservationModel, Reservation>().ReverseMap();
            CreateMap<Reservation, ReservationResponseModel>().ReverseMap();

            #endregion
        }
    }
}
