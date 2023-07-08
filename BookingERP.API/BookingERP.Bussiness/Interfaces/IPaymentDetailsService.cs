using BookingERP.Bussiness.Models.PaymentDetailsModel;

namespace BookingERP.Bussiness.Interfaces
{
    public interface IPaymentDetailsService
    {
        Task AddAsync(PaymentDetailsModel paymentDetails);
        Task UpdateAsync(PaymentDetailsResponseModel paymentDetails);
        Task DeleteAsync(Guid id);
        
    }
}
