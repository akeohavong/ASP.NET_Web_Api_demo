using Microsoft.AspNetCore.Mvc;
using SimpleSchool.Core.Entities;
using SimpleSchool.Core.Interfaces;
using SimpleSchool.Mvc.Models;

namespace SimpleSchool.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IBuildingRepository buildingRepository, IRoomRepository roomRepository)
        {
            _buildingRepository = buildingRepository;
            _roomRepository = roomRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/{id}", Name ="GetRoom")]
        public IActionResult GetRoom(int id)
        {
            var result = _roomRepository.Get(id);
            if (result.Success)
            {
                return Ok(new RoomModel()
                {
                    RoomID = result.Data.RoomID,
                    RoomNumber = result.Data.RoomNumber,
                    Description = result.Data.Description, 
                });
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPost]
        public IActionResult AddRoom(int buildingId)
        {
            Room r = new Room();
            r.BuildingID = buildingId;

            var result = _roomRepository.Add(r);
            if (result.Success)
            {
                return CreatedAtRoute(nameof(GetRoom), new { id = r.RoomID }, r);
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpPut]
        public IActionResult EditRoom(Room room)
        {
            if (!_roomRepository.Get(room.RoomID).Success)
            {
                return NotFound($"Room {room.RoomID} not found");
            }
            var result = _roomRepository.Edit(room);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            if(!_roomRepository.Get(id).Success)
            {
                return NotFound($"Room {id} not found");
            }

            var result = _roomRepository.Delete(id);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Messages);
            }
        }
        
    }
}
