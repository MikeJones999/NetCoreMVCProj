using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    public class TripsController : Controller
    {
        private IWorldRepository _context;

        //[HttpGet("api/trips")]
        //public JsonResult Get()
        //{
        //    return Json( new Trip() { Name = "My Trip" });
        //}

        public TripsController(IWorldRepository context)
        {
            _context = context;
        }


        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            return Ok(_context.GetAllTrips());
        }
    }
}
