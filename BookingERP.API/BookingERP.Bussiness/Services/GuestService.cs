using AutoMapper;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Manager;
using BookingERP.Common.Exceptions;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookingERP.Bussiness.Services
{
    public class GuestService : IGuestService
    {

        private readonly IGuestRepository _guestRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public GuestService(IGuestRepository guestRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateGuestAsync(ApplicationUser user, GuestRegisterModel model)
        {
            Guest newGuest = _mapper.Map<Guest>(model);
            newGuest.User = user;
            await _guestRepository.AddAsync(newGuest);
        }

        public async Task<GuestResponseModel> GetGuestById(Guid id)
        {
            var guest = await _guestRepository.GetGuestById(id);
            if (guest == null)
            {
                throw new ResourceNotFoundException($"There is no user with id: {id}");
            }

            return _mapper.Map<GuestResponseModel>(guest);
        }
    }
}
