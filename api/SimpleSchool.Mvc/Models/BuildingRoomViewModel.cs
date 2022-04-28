using SimpleSchool.Core.Entities;
using System.Collections.Generic;

namespace SimpleSchool.Mvc.Models
{
    public class BuildingRoomViewModel
    {
        public Building Building { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
