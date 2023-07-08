using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;

namespace BookingERP.Data.Repositories
{
    public class PaymentDetailsRepository : GenericRepository<PaymentDetails>,  IPaymentDetailsRepository
    {
        public PaymentDetailsRepository(BookingContext context) : base(context)
        {
        }
    }
}
