using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Discount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> AddAsync(DiscountModel discount)
        {
            await _discountService.AddAsync(discount);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdLazy(string id)
        {
            var discount = await _discountService.GetByIdLazy(Guid.Parse(id));
            return Ok(discount);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _discountService.DeleteAsync(Guid.Parse(id));
            return Ok();

        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(DiscountModel discount)
        {
            await _discountService.UpdateAsync(discount);
            return Ok();
        }
    }
}
