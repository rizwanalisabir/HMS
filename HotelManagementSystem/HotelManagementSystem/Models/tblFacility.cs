using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HotelManagementSystem.Models
{
    public class tblFacility
    {
        public int ID { get; set; }

        public string FacilityName { get; set; }

        public bool IsAvailable { get; set; }

        public string Field1 { get; set; }

        public string Field2 { get; set; }

        public string Field3 { get; set; }

        public string Field4 { get; set; }

        public string Field5 { get; set; }

        public int Field6 { get; set; }

        public int CB { get; set; }

        public int MB { get; set; }

        public int DB { get; set; }

        public DateTime CD { get; set; }
    
        public DateTime MD { get; set; }

        public DateTime DD { get; set; }

    }

}
