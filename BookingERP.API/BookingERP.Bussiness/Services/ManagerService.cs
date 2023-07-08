using AutoMapper;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Manager;
using BookingERP.Common.Exceptions;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookingERP.Bussiness.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepostirory _managerRepostirory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ManagerService(IMapper mapper,  IManagerRepostirory managerRepostirory, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _managerRepostirory = managerRepostirory;
            _userManager = userManager;
        }

        public async Task CreateManagerAsync(ApplicationUser user, ManagerRegisterModel model)
        {
            Manager newManager = _mapper.Map<Manager>(model);
            newManager.User = user;
            
            await _managerRepostirory.AddAsync(newManager);
        }
       public async Task<ManagerResponseModel> GetMaganerByIdAsync(Guid id)
        {
            var manager = await _managerRepostirory.GetMaganerByIdAsync(id);
            if (manager == null)
            {
                throw new ResourceNotFoundException($"There is no manager with id: {id}");
            }

            return _mapper.Map<ManagerResponseModel>(manager);
        }

        public async Task<IEnumerable<ManagerResponseModel>> GetAllManagersByHotelId(Guid hotelId)
        {
            var managers = await _managerRepostirory.GetManagersByHotelId(hotelId);

            var managersList = new List<ManagerResponseModel>();
            foreach (var manager in managers)
            {
                managersList.Add(_mapper.Map<ManagerResponseModel>(manager));
            }

            return managersList;
        }
    }
}
