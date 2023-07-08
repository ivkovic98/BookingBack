namespace BookingERP.Bussiness.Models.Reservation
{
    public class ReservationModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int Capacity { get; set; }

        public bool Paid { get; set; } 
        public string  GuestId { get; set; }
        public List<string> RoomsIds { get; set; }
        public string HotelId { get; set; }

    }
}
