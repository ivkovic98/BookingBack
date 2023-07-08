using BookingERP.Bussiness.Interfaces;
using BookingERP.Bussiness.Models.Room;
using BookingERP.Bussiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddAsync(RoomModel room)
        {
            await _roomService.AddAsync(room);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _roomService.DeleteAsync(Guid.Parse(id));
            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(string id)
        {
            var room = await _roomService.GetRoomById(Guid.Parse(id));
            return Ok(room);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RoomModel room)
        {
            await _roomService.UpdateAsync(room);
            return Ok();
        }

        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("get-hotel-rooms/{id}")]
        public async Task<IActionResult> GetRoomsByHotelId(string id)
        {
            var rooms = await _roomService.GetRoomsByHotelId(Guid.Parse(id));
            return Ok(rooms);
        }

    }
}
