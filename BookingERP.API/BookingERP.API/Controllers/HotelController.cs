using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddAsync(HotelModel hotel)
        {
            await _hotelService.AddAsync(hotel);

            return Ok();

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotel = await _hotelService.GetAllHotels();
            return Ok(hotel);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelByIdAsync(string id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(Guid.Parse(id));
            return Ok(hotel);

        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync (string id)
        {
            await _hotelService.DeleteAsync(Guid.Parse(id));
            return Ok();

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync (HotelModel hotel)
        {
           await _hotelService.UpdateAsync(hotel);
           return Ok();
        }

        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get-hotel-guests/{id}")]
        public async Task<IActionResult> GetGuestsByHotelId(string id)
        {
            var hotel = await _hotelService.GetAllGuestsByHotelId(Guid.Parse(id));
            return Ok(hotel);
        }


        //TODO:
       
        //Reservation Controller
        // I want to make a reservation for a hotel with Start Date and End Date and Room
        // As a manager I want to see all reservations for my hotel
        // As a guest I want to see all reservations i made for all hotels
        // As a guest I want to cancel my reservation 
    }
}
