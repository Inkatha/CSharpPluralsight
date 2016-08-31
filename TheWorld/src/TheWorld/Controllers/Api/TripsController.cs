using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewsModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var trips = _repository.GetUserTripsWithStops(User.Identity.Name);
                return Json(trips);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get all trips: {ex}");
                return BadRequest("Error Occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel newTrip)
        {
            if (ModelState.IsValid)
            {
                newTrip.UserName = User.Identity.Name;
                var trip = Mapper.Map<Trip>(newTrip);
                

                _logger.LogInformation("Attempting to save a new trip");

                _repository.AddTrip(trip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{newTrip.Name}", Mapper.Map<TripViewModel>(trip));
                }
                else
                {
                    return BadRequest("Failed to save changes to the database.");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
