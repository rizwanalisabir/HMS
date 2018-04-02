using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class FacilityController : Controller
    {
        DataAccessLayer DAL = new DataAccessLayer();

        // GET: api/<controller>
        [HttpGet]
        [Route("api/Facility/GetFacilities")]
        public IEnumerable<tblFacility> GetFacilities()
        {
            return DAL.GetFacilities();
        }

        [HttpGet]
        [Route("api/Facility/GetFacilityNames")]
        public Dictionary<string, bool> GetFacilityNames()
        {
            IEnumerable<tblFacility> list = DAL.GetFacilities();
            Dictionary<string,bool> nameslist = new Dictionary<string, bool>();
            foreach (var item in list)
            {
                nameslist.Add(item.FacilityName,item.IsAvailable);
            }
            return nameslist;
        }

        [HttpDelete]
        [Route("api/Facility/Delete/{id}")]
        public int Delete(int id)
        {
            return DAL.DeleteFacility(id);
        }
        

        [HttpPost]
        [Route("api/Facility/Create")]
        public JsonResult Create([FromHeader]tblFacility ReceiverClass)
        {
            try
            {
                if (DAL.AddFacility(ReceiverClass) == 1)
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
        [Route("api/Facility/Edit")]
        public JsonResult Edit([FromHeader]tblFacility ReceiverClass)
        {
            try
            {
                if (DAL.UpdateFacility(ReceiverClass) == 1)
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
    }
}