using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public  ReservationController (IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //[Authorize(Roles = "Admin, Manager,Guest")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddAsync(ReservationModel model)
        {
            await _reservationService.AddAsync(model);

            return Ok();

        }

        //[Authorize(Roles = "Admin, Manager,Guest")]
        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReservationById(string id)
        {
            var reservation = await _reservationService.GetReservationById(Guid.Parse(id));

            return Ok(reservation);
        }


        //[Authorize(Roles = "Admin, Manager,Guest")]
        [AllowAnonymous]
        [HttpGet]
        [Route("get-guest-resevations/{id}")]
        public async Task<IActionResult> GetGuestReservations(string id)
        {
            var reservation = await _reservationService.GetReservationsByGuestId(Guid.Parse(id));

            return Ok(reservation);
        }

        //[Authorize(Roles = "Admin, Manager,Guest")]
        [AllowAnonymous]
        [HttpGet]
        [Route("get-hotel-resevations/{id}")]
        public async Task<IActionResult> GetHotelReservations(string id)
        {
            var reservation = await _reservationService.GetReservationsByHotelId(Guid.Parse(id));

            return Ok(reservation);
        }

        //[Authorize(Roles = "Admin, Manager,Guest")]
        [AllowAnonymous]
        [HttpGet]
        [Route("checkReservation")]
        public async Task<IActionResult> CheckReservationAvailability(DateTime startDate, DateTime endDate)
        {
            var availableRooms = await _reservationService.CheckAvailableRooms(startDate, endDate);

            return Ok(availableRooms);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _reservationService.DeleteAsync(Guid.Parse(id));
            return Ok();
        }


    }
}
