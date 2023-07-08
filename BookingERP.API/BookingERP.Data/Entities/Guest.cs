namespace BookingERP.Data.Entities
{
    public class Guest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string?  Surname { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
