namespace BookingERP.Bussiness.Models.Manager
{
    public class ManagerResponseModel
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
