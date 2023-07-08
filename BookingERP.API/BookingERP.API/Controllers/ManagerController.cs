using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMaganerByIdAsync(string id)
        {
            var manager = await _managerService.GetMaganerByIdAsync(Guid.Parse(id));
            return Ok(manager);
        }

        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get-hotel-managers/{id}")]
        public async Task<IActionResult> GetManagersByHotelId(string id)
        {
            var hotel = await _managerService.GetAllManagersByHotelId(Guid.Parse(id));
            return Ok(hotel);
        }

    }
}
