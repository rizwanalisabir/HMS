using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class RoomBookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}