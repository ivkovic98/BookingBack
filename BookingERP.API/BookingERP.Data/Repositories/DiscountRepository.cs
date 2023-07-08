using BookingERP.Data.Context;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;

namespace BookingERP.Data.Repositories
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(BookingContext context) : base(context)
        {
        }

    }
}
