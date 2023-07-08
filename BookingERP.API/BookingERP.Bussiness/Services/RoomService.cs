using AutoMapper;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Room;
using BookingERP.Common.Exceptions;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;

namespace BookingERP.Bussiness.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public RoomService(IMapper mapper,IRoomRepository roomRepository, IHotelRepository hotelRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
        }

        public async Task AddAsync(RoomModel room)
        {
            var rooms = await _hotelRepository.GetAllHotelRooms(room.HotelId);

            if(rooms.Any(r=>r.RoomNumber == room.RoomNumber))
            {
                throw new RoomNumberConflict("Hotel already has a room with that room number");
            }

            Room newRoom = _mapper.Map<Room>(room);
            await _roomRepository.AddAsync(newRoom);
        }

       public async Task DeleteAsync(Guid id)
        {
           await _roomRepository.DeleteAsync(id);
        }

       public async Task<RoomModel> GetRoomById(Guid id)
         {
            var room = await _roomRepository.GetRoomById(id);
             if(room == null)
             {
                 throw new Exception($"There is no room with id: {id}");
             }
            var response = _mapper.Map<RoomModel>(room);
             return response;
         }

        public async Task UpdateAsync(RoomModel room)
        {
            Room roomToUpdate = _mapper.Map<Room>(room);
            await _roomRepository.UpdateAsync(roomToUpdate);
        }


        public async Task<IEnumerable<RoomModel>> GetRoomsByHotelId(Guid hotelId)
        {
            var rooms = await _roomRepository.GetAllRoomsByHotelId(hotelId);

            var roomList = new List<RoomModel>();
            foreach (var room in rooms)
            {
                roomList.Add(_mapper.Map<RoomModel>(room));
            }
            return roomList;
        }
    }
}
