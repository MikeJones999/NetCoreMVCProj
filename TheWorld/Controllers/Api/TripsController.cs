using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{

    [Route("api/trips")]  //use this to default the route
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


        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_context.GetAllTrips());
        }


        [HttpPut("")]
        //[HttpPut("api/trips")]
        public IActionResult Put([FromBody] Trip trip)
        {
            return Ok(true);
        }

    }
}
