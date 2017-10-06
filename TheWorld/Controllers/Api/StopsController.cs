using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;
        private GeoCoordsService _coordsService;

        public StopsController(ILogger<StopsController> logger, IWorldRepository repository, GeoCoordsService coordsService) 
        {
            _logger = logger;
            _repository = repository;
            _coordsService = coordsService;
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
                    var results = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!results.Success)
                    {
                        _logger.LogError(results.Message);
                    }
                    else
                    {
                        newStop.Latitude = results.Latitude;
                        newStop.Longitude = results.Longitude;

                        //save to Database

                        _repository.AddStop(tripName, newStop);
                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                        }
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
