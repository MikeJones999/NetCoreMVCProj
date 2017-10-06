using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

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
            try
            {
                var results = _context.GetAllTrips();

                //use mapping to return results as view model - bit of abstraction to hide the full detail
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch(Exception ex)
            {
                //TODO Logging

                return BadRequest("Error occured : " + ex.Message);
            }
        }


        //[HttpPut("")]
        ////[HttpPut("api/trips")]
        //public IActionResult Put([FromBody] Trip trip)
        //{
        //    return Ok(true);
        //}


            //as we want to hide information (returned) use View Models rather than the entity itself - also the validation isnt required on the entity rather
            //used on the view model
            //below returns the viewModel not the entity 
        [HttpPut("")]
        public IActionResult Put([FromBody] TripViewModel trip)
        {
            if(ModelState.IsValid)
            {

                //using automapper to passin a tripview model and map to a trip entity
                var newTrip = Mapper.Map<Trip>(trip);

                //send back the new trip but as vewi model as not to expose the detail
                return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
            }
            //return BadRequest("Incorrect format on Trip");
            return BadRequest(ModelState); //******dont use in public use - debug only*******
        }

    }
}
