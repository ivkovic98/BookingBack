using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Guest;
using BookingERP.Bussiness.Models.Login;
using BookingERP.Bussiness.Models.Manager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authenticationService.Login(model);
            return Ok(response);
        }

        [HttpPost]
        [Route("register/guest")]
        public async Task<IActionResult> RegisterGuest([FromBody] GuestRegisterModel model)
        {
            await _authenticationService.RegisterGuest(model);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("register/manager")]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerRegisterModel model)
        {
            await _authenticationService.RegisterManager(model);
            return Ok();
        }
    }
}
