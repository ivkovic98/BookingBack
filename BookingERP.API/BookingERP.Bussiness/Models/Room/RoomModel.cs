namespace BookingERP.Bussiness.Models.Room
{
    public class RoomModel 
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public int Capacity { get; set; }
    }
}
