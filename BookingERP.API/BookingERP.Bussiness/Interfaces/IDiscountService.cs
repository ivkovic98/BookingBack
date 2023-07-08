using BookingERP.Bussiness.Models.Discount;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IDiscountService
    {
        Task AddAsync(DiscountModel discount);
        Task UpdateAsync(DiscountModel discount);
        Task DeleteAsync(Guid id);
        Task <DiscountModel> GetByIdLazy(Guid id);

    }
}
