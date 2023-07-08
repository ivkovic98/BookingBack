namespace BookingERP.Data.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string DateOfPayment { get; set; }
        public string AmountOfMoney { get; set; } 
        public Guid PaymentDetailsId { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
    }
}
