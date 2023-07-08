using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Discount;
using BookingERP.Data.Entities;
using BookingERP.Data.Interfaces;

namespace BookingERP.Bussiness.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountService(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        public async Task AddAsync(DiscountModel discount)
        {
            Discount newDiscount = new()
            {
                DiscountType = discount.DiscountType,
            };
            await _discountRepository.AddAsync(newDiscount);
        }

       

        public async Task DeleteAsync(Guid id)
        {
            await _discountRepository.DeleteAsync(id);
        }

        public async Task<DiscountModel> GetByIdLazy(Guid id)
        {
            var discount= await _discountRepository.GetByIdLazy(id);
            if(discount == null)
            {
                throw new Exception($"There is no discount with id: {id}");
            }
            var response = new DiscountModel()
            {
                Id = id,
                DiscountType = discount.DiscountType,

            };
            return response;
        }

        public async Task UpdateAsync(DiscountModel discount)
        {
            Discount discountToUpdate = new()
            {
                Id = discount.Id,
                DiscountType = discount.DiscountType,
            };
            await _discountRepository.UpdateAsync(discountToUpdate);
        }
    }
}
