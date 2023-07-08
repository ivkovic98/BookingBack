using AutoMapper;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Hotel;
using BookingERP.Bussiness.Models.Manager;
using BookingERP.Bussiness.Models.Room;
using BookingERP.Common.Exceptions;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;

namespace BookingERP.Bussiness.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IManagerRepostirory _managerRepostirory;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public HotelService(IMapper mapper, IRoomRepository roomRepository, IHotelRepository hotelRepository, IManagerRepostirory managerRepostirory, IGuestRepository guestRepository)
        {
            _mapper = mapper;
            _hotelRepository = hotelRepository;
            _managerRepostirory = managerRepostirory;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
        }

        public async Task AddAsync(HotelModel hotel)
        {
            Hotel newHotel = _mapper.Map<Hotel>(hotel);
            await _hotelRepository.AddAsync(newHotel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _hotelRepository.DeleteAsync(id);

        }

        public async Task<IEnumerable<GuestResponseModel>> GetAllGuestsByHotelId(Guid id)
        {
            var guests = await _hotelRepository.GetAllGuestsByHotelId(id);
            
            var guestList = new List<GuestResponseModel>();
            foreach (var guest in guests)
            {
                guestList.Add(_mapper.Map<GuestResponseModel>(guest));
            }

            return guestList;
        }

        public async Task<IEnumerable<HotelModel>> GetAllHotels()
        {
            var hotels = await _hotelRepository.GetAllAsync();

            var hotelList = new List<HotelModel>();
            foreach (var hotel in hotels)
            {
                hotelList.Add(_mapper.Map<HotelModel>(hotel));
            }

            return hotelList;
        }

        public async Task<HotelModel> GetHotelByIdAsync(Guid Id)
        {
            var hotel = await _hotelRepository.GetHotelById(Id);
            if (hotel == null)
            {
                throw new ResourceNotFoundException($"There is no hotel with id: {Id}");
            }

            return _mapper.Map<HotelModel>(hotel);
        }

        public async Task UpdateAsync(HotelModel hotel)
        {
            Hotel hotelToUpdate = _mapper.Map<Hotel>(hotel);
            await _hotelRepository.UpdateAsync(hotelToUpdate);
        }



    }
}
