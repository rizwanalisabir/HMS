using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HotelManagementSystem.Models
{
    public class tblRoomInfo
    {
        public string CustomerName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string IdCardNumber { get; set; }

        public string ContactNumber { get; set; }

        public decimal AdvancePayment { get; set; }

        public string PaymentStatus { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime FromeDateTime { get; set; }

        public DateTime ToDateTime { get; set; }

        public int RemainingDays { get; set; }

        public int RoomPriceID { get; set; }

        public decimal FeePerDay { get; set; }

        public decimal FeePerWeek { get; set; }

        public decimal FeePerMonth { get; set; }

        public decimal PercentageChange { get; set; }

        public string RoomPriceField1 { get; set; }

        public string RoomPriceField2 { get; set; }

        public string RoomPriceField3 { get; set; }

        public string RoomPriceField4 { get; set; }

        public string RoomPriceField5 { get; set; }

        public int RoomPriceField6 { get; set; }

        public int RoomFacilitID { get; set; }

        public string FacilityName { get; set; }

        public bool IFacilityAvailable { get; set; }

        public string RoomFacilityField1 { get; set; }

        public string RoomFacilityField2 { get; set; }

        public string RoomFacilityField3 { get; set; }

        public string RoomFacilityField4 { get; set; }

        public string RoomFacilityField5 { get; set; }

        public int RoomFacilityField6 { get; set; }

        public int RoomID { get; set; }

        public string Floor { get; set; }

        public string RoomNo { get; set; }

        public int NoOfBed { get; set; }

        public int NoOfAdult { get; set; }

        public int NoOfChild { get; set; }

        public string RoomType { get; set; }
        
        public string Status { get; set; }

        public string Description { get; set; }

        public string RoomField1 { get; set; }

        public string RoomField2 { get; set; }

        public string RoomField3 { get; set; }

        public string RoomField4 { get; set; }

        public string RoomField5 { get; set; }

        public int RoomField6 { get; set; }

        public int CB { get; set; }

        public int MB { get; set; }

        public int DB { get; set; }

        public DateTime CD { get; set; }

        public DateTime MD { get; set; }

        public DateTime DD { get; set; }

        public string Facilities { get; set; }

        public string Image { get; set; }

        public DateTime BookingDate { get; set; }
    }
}
