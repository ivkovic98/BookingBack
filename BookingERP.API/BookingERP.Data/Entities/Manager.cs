namespace BookingERP.Data.Entities
{
    public class Manager
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public ApplicationUser? User { get; set; }
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

    }
}
