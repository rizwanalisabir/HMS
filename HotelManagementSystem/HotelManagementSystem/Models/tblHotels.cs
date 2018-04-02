using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models
{
    public class tblHotels
    {
        public int ID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string LisenceNo { get; set; }
        public string Logo { get; set; }
        public string PhoneNumberFirst { get; set; }
        public string PhoneNumberSecond { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string HeaderNotes { get; set; }
        public string FooterNotes { get; set; }
        public string SpecialNotes { get; set; }
    }
}
