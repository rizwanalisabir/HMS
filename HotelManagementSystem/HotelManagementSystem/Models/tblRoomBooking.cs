using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models
{
    public class tblRoomBooking
    {
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string PriceRange { get; set; }
        public int NoOfBeds { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChild { get; set; }
        public string RoomType { get; set; }
        public string RoomFacilities { get; set; }
    }
}
