using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class PointOfInterestController : Controller
	{
		private ILogger<PointOfInterestController> _logger;
		private IMailService _mailService;

		public PointOfInterestController(ILogger<PointOfInterestController> logger,
			LocalMailService mailService)
		{
			_logger = logger;
			_mailService = mailService;
		}

		[HttpGet("{cityId}/pointsofinterest")]
		public IActionResult GetPointsOfInterest(int cityId)
		{
			try
			{
				var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

				if (city == null)
				{
					_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
					return NotFound();
				}
				return Ok(city.PointsOfInterest);
			}
			catch (Exception ex)
			{
				_logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
				return StatusCode(500, "A problem happened while handling your request.");
			}
		}

		[HttpGet("{cityId}/pointsofinterest/{id}")]
		public IActionResult GetPointOfInterest(int cityId, int id)
		{
			var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

			if (pointOfInterest == null)
			{
				return NotFound();
			}

			return Ok(pointOfInterest);
		}

		[HttpPost("{cityId}/pointsofinterest")]
		public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
		{
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			if (pointOfInterest.Description.Equals(pointOfInterest.Description, StringComparison.OrdinalIgnoreCase))
			{
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var maxPointOfInterestId = CityDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

			var finalPointOfInterest = new PointOfInterestDto()
			{
				Id = ++maxPointOfInterestId,
				Name = pointOfInterest.Name,
				Description = pointOfInterest.Description
			};

			return Ok();
		}

		[HttpPut("{cityId}/pointsofinterest/{id}")]
		public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
		{
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			// This check only needs to be performed if both description and name are not null
			if (pointOfInterest.Description != null && pointOfInterest.Name != null)
			{
				if (pointOfInterest.Description.Equals(pointOfInterest.Name, StringComparison.OrdinalIgnoreCase))
				{
					ModelState.AddModelError("Description", "The name and description cannot be updated to the same value.");
				}
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterstFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

			if (pointOfInterstFromStore == null)
			{
				return NotFound();
			}

			pointOfInterstFromStore.Name = pointOfInterest.Name;
			pointOfInterstFromStore.Description = pointOfInterest.Description;

			return NoContent();
		}

		[HttpPatch("{cityId}/pointsofinterest/{id}")]
		public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
			[FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
		{
			if (patchDoc == null)
			{
				return BadRequest();
			}

			var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			var pointofInterestToPatch = new PointOfInterestForUpdateDto()
			{
				Name = pointOfInterestFromStore.Name,
				Description = pointOfInterestFromStore.Description
			};

			patchDoc.ApplyTo(pointofInterestToPatch, ModelState);

			// This check only needs to be performed if both description and name are not null
			if (pointofInterestToPatch.Description != null && pointofInterestToPatch.Name != null)
			{
				if (pointofInterestToPatch.Description.Equals(pointofInterestToPatch.Name, StringComparison.OrdinalIgnoreCase))
				{
					ModelState.AddModelError("Description", "The name and description cannot be updated to the same value.");
				}
			}

			TryValidateModel(pointofInterestToPatch);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			pointOfInterestFromStore.Name = pointofInterestToPatch.Name;
			pointOfInterestFromStore.Description = pointofInterestToPatch.Description;

			return NoContent();
		}

		[HttpDelete("{cityId}/pointsofinterest/{id}")]
		public IActionResult DeletePointOfInterest(int cityId, int id)
		{
			var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
			if (city == null)
			{
				return NotFound();
			}

			var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
			if (pointOfInterestFromStore == null)
			{
				return NotFound();
			}

			city.PointsOfInterest.Remove(pointOfInterestFromStore);

			_mailService.Send("Point of interest deleted.",
				$"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted.");

			return NoContent();
		}
	}
}


