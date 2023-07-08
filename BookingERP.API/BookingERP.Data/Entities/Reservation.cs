namespace BookingERP.Data.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }
        public bool Paid { get; set; }
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; }

        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<ReservationRoom> ReservationRooms { get; set; }

    }
}
