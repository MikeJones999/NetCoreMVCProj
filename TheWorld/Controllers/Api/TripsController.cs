using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<Trip> _logger;

        //[HttpGet("api/trips")]
        //public JsonResult Get()
        //{
        //    return Json( new Trip() { Name = "My Trip" });
        //}

        public TripsController(IWorldRepository context, ILogger<Trip> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogError($"Failed to get all trips from get request: {ex}");
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
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] TripViewModel trip)
        {
            if(ModelState.IsValid)
            {

                //using automapper to passin a tripview model and map to a trip entity
                var newTrip = Mapper.Map<Trip>(trip);
                _context.AddTrip(newTrip);


                if(await _context.SaveChangesAsync())
                {
                    //send back the new trip but as vewi model as not to expose the detail
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save changes to Database");
                }
            }
            //return BadRequest("Incorrect format on Trip");
            return BadRequest(ModelState); //******dont use in public use - debug only*******
        }

    }
}
