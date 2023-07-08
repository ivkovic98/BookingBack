namespace BookingERP.Data.Entities
{
    public class PaymentDetails
    {
        public Guid Id { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
