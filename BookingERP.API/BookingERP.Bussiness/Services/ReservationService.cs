using AutoMapper;
using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Reservation;
using BookingERP.Bussiness.Models.Room;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;

namespace BookingERP.Bussiness.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IGuestService _guestService;
        private readonly IMapper _mapper;


        public ReservationService(IMapper mapper, IReservationRepository reservationRepository, IGuestService guestService)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _guestService = guestService;
        }

        public async Task<ReservationResponseModel> GetReservationById(Guid id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);
            var guest = await _guestService.GetGuestById(reservation.GuestId);
            
            var rooms = new List<RoomModel>();

            foreach (var rr in reservation.ReservationRooms)
            {
                rooms.Add(_mapper.Map<RoomModel>(rr.Room));
            }

            return new ReservationResponseModel()
            {
                Id = reservation.Id,
                Capacity = reservation.Capacity,
                EndDate = reservation.EndDate,
                Guest = guest,
                Price = reservation.Price,
                Rooms = rooms
            };
        }


        public async Task AddAsync(ReservationModel model)
        {
            try
            {
                //check if room still available
                var rooms = await _reservationRepository.GetAvailableRooms(model.StartDate, model.EndDate);

                foreach (var id in model.RoomsIds)
                {
                    if(!rooms.Any(r => r.Id == Guid.Parse(id)))
                    {
                        throw new Exception("One of the rooms is not available anymore!");
                    }
                }

                Reservation newReservation = _mapper.Map<Reservation>(model);

                var reservationRooms = model.RoomsIds.Select(roomId => new ReservationRoom
                {
                    Reservation = newReservation,
                    RoomId = Guid.Parse(roomId)
                }).ToList();

                await _reservationRepository.CreateReservation(newReservation, reservationRooms);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<RoomModel>> CheckAvailableRooms(DateTime startDate, DateTime endDate)
        {
            var availableRooms = await _reservationRepository.GetAvailableRooms(startDate, endDate);
            List<RoomModel> roomList = new List<RoomModel>();

            foreach(var room in availableRooms)
            {
                roomList.Add(new RoomModel()
                {
                    HotelId = room.HotelId,
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    RoomType = room.RoomType,
                    Capacity = room.Capacity
                });
            }

            return roomList;
        }

        public async Task DeleteAsync(Guid Id)
        {
            await _reservationRepository.DeleteAsync(Id);
        }

        public async Task<IEnumerable<ReservationResponseModel>> GetReservationsByGuestId(Guid Id)
        {
            var reservations = await _reservationRepository.GetReservationsByGuestId(Id);

            List<ReservationResponseModel> reservationList = new List<ReservationResponseModel>();

            foreach( var reservation in reservations)
            {
                reservationList.Add(_mapper.Map<ReservationResponseModel>(reservation));
            }

            return reservationList;
        }

        public async Task<IEnumerable<ReservationResponseModel>> GetReservationsByHotelId(Guid Id)
        {
            var reservations = await _reservationRepository.GetReservationsByHotelId(Id);

            List<ReservationResponseModel> reservationList = new List<ReservationResponseModel>();

            foreach (var reservation in reservations)
            {
                reservationList.Add(_mapper.Map<ReservationResponseModel>(reservation));
            }

            return reservationList;
        }
    }
}
