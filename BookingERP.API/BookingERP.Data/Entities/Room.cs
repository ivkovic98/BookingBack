namespace BookingERP.Data.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}
