namespace BookingERP.Bussiness.Models.PaymentDetailsModel
{
    public class PaymentDetailsModel
    {
        public int Id { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public int PaymentId { get; set; }
    }
}
