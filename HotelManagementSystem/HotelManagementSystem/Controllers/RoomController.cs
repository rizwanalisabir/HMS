using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Models;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagementSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly IHostingEnvironment _environment;
        DataAccessLayer DAL = new DataAccessLayer();

        // GET: api/<controller>
        [HttpGet]
        [Route("api/Room/Index")]
        public IEnumerable<tblRoomInfo> Index()
        {
            return DAL.GetAllRooms();
        }

        [HttpGet]
        [Route("api/Room/Details/{id}")]
        public tblRoomInfo Details(int id)
        {
            return DAL.GetRoom(id);
        }

        [HttpDelete]
        [Route("api/Room/Delete/{id}")]
        public int Delete(int id)
        {
            return DAL.DeleteRoom(id);
        }

        [HttpGet]
        [Route("api/Room/HotelDetails/{id}")]
        public tblHotels HotelDetails(string id)
        {
            return DAL.GetHotelDetails(id);
        }

        [HttpPut]
        [Route("api/Room/UpdateHotel")]
        public void UpdateHotel([FromBody]tblHotels hotel)
        {
            DAL.UpdateHotel(hotel);
        }

        [HttpPut]
        [Route("api/Room/UpdateBookedRoom/{id}")]
        public void UpdateBookedRoom(int id)
        {
            DAL.UpdateBookedRoom(id);
        }

        [HttpPost]
        [Route("api/Room/FilterRooms")]
        public IEnumerable<tblRoomInfo> FilterRooms([FromBody]tblRoomInfo filters)
        {
            return DAL.FilterRooms(filters);
        }

        [HttpPost]
        [Route("api/Room/FilterRoomsbyCustomer")]
        public IEnumerable<tblRoomBooking> FilterRoomsbyCustomer([FromBody]tblRoomBooking filters)
        {
            return DAL.FilterRoomsbyCustomer(filters);
        }

        [HttpPost]
        [Route("api/Room/Create")]
        public JsonResult Create([FromHeader]tblRoomInfo ReceiverClass)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                string PathDB = string.Empty;
                if (files != null && files.Count == 1)
                {
                    string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string imagepath = root + @"\dist\RoomImages";
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var FileExtension = Path.GetExtension(fileName);
                            var newFileName = myUniqueFileName + FileExtension;
                            fileName = imagepath + $@"\{newFileName}";
                            PathDB = "/dist/RoomImages/" + newFileName;
                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                                ReceiverClass.Image = newFileName;
                            }
                        }
                    }
                }
                if (DAL.AddRoom(ReceiverClass) == 1)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpPut]
        [Route("api/Room/Edit")]
        public JsonResult Edit([FromHeader]tblRoomInfo ReceiverClass)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                string PathDB = string.Empty;
                if (files != null && files.Count == 1)
                {
                    string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    string imagepath = root + @"\dist\RoomImages";
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var FileExtension = Path.GetExtension(fileName);
                            var newFileName = myUniqueFileName + FileExtension;
                            fileName = imagepath + $@"\{newFileName}";
                            PathDB = "/dist/RoomImages/" + newFileName;
                            using (FileStream fs = System.IO.File.Create(fileName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                                ReceiverClass.Image = newFileName;
                            }
                        }
                    }
                }
                if (DAL.UpdateRoom(ReceiverClass) == 1)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }
        

        [HttpGet]
        [Route("api/Room/GetBookedRooms")]
        public IEnumerable<tblRoomInfo> GetBookedRooms(string id)
        {
            return DAL.ShowBookedRooms();
        }
        //[HttpGet]
        //[Route("api/Room/UpdateBookedRoom")]
        //public void UpdateBookedRoom(int id)
        //{
        //    DAL.UpdateBookedRoom(id);
        //}

        [HttpPost]
        [Route("api/Room/RoomBookingByAdmin")]
        public int RoomBookingByAdmin([FromBody]tblRoomInfo bookingdetails)
        {
            return DAL.RoomBookingByAdmin(bookingdetails);
        }
    }
}
