using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Hotel;
using BookingERP.Bussiness.Models.Room;

namespace BookingERP.Bussiness.Models.Reservation
{
    public class ReservationResponseModel 
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
        public bool Paid { get; set; }
        public List<RoomModel> Rooms { get; set; }
        public string HotelId { get; set; }
        public HotelModel Hotel{ get; set; }

        public GuestResponseModel Guest { get; set; }
    }
}
