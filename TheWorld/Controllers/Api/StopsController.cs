using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(ILogger<StopsController> logger, IWorldRepository repository) 
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get stops: {0}", ex);    
            }
            return BadRequest($"Failed to get Trip: {tripName}");
        }


        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName,[FromBody] StopViewModel stop)
        {
            try
            {
                if(ModelState.IsValid)
                {
                   var newStop = Mapper.Map<Stop>(stop);

                    //lookup the geocodes


                    //save to Database

                    _repository.AddStop(tripName, newStop);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new Stop: {0}", ex);
            }
            return BadRequest("Failed to save new Stop");
        }
    }
}
