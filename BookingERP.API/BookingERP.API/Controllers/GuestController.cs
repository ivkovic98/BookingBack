using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly  IGuestService _guestService;
        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;

        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestByIdAsync(string id)
        {
            var guest = await _guestService.GetGuestById(Guid.Parse(id));
            return Ok(guest);
        }

    }
}
