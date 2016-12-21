using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;
using CityInfo.API.Services;
using CityInfo.API.Entities;
using System.Collections.Generic;
using AutoMapper;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class PointOfInterestController : Controller
	{
		private ILogger<PointOfInterestController> _logger;
		private IMailService _mailService;
		private ICityInfoRepository _cityInfoRepository;

		public PointOfInterestController(ILogger<PointOfInterestController> logger,
			IMailService mailService, ICityInfoRepository cityInfoRepository)
		{
			_logger = logger;
			_mailService = mailService;
			_cityInfoRepository = cityInfoRepository;
		}

		[HttpGet("{cityId}/pointsofinterest")]
		public IActionResult GetPointsOfInterest(int cityId)
		{
			try
			{
				if (!_cityInfoRepository.CityExists(cityId))
				{
					_logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
					return NotFound();
				}

				var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfInterestForCity(cityId);

				var pointsOfInterestForCityResults = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity);

				return Ok(pointsOfInterestForCityResults);
			}
			catch (Exception ex)
			{
				_logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
				return StatusCode(500, "A problem happened while handling your request.");
			}
		}

		[HttpPost("{cityId}/pointsofinterest")]
		public IActionResult CreatePointOfInterest(int cityId, 
			[FromBody] PointOfInterestForCreationDto pointOfInterest)
		{
			if (pointOfInterest == null)
			{
				return BadRequest();
			}

			if (pointOfInterest.Name.Equals(pointOfInterest.Description, StringComparison.OrdinalIgnoreCase))
			{
				ModelState.AddModelError("Description", "The provided description should be different from the name.");
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!_cityInfoRepository.CityExists(cityId))
			{
				return NotFound();
			}

			var finalPointOfInterest = Mapper.Map<PointOfInterest>(pointOfInterest);

			_cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

			if (!_cityInfoRepository.Save())
			{
				return StatusCode(500, "A problem happened while handling your request.");
			}

			var createdPointOfInterestToReturn = Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

			return CreatedAtRoute("GetPointOfInterest", new
			{
				cityId = cityId,
				id = createdPointOfInterestToReturn.Id
			}, createdPointOfInterestToReturn);
		}

		[HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
		public IActionResult GetPointOfInterest(int cityId, int id)
		{
			try
			{
				if (!_cityInfoRepository.CityExists(cityId))
				{
					return NotFound();
				}

				var pointOfInterestForCity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

				if (pointOfInterestForCity == null)
				{
					return NotFound();
				}

				var pointOfInterestForCityResult = Mapper.Map<PointOfInterestDto>(pointOfInterestForCity);

				return Ok(pointOfInterestForCityResult);
			}
			catch (Exception ex)
			{
				_logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
				return StatusCode(500, "A problem happened while handling your request.");
			}
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

			if (!_cityInfoRepository.CityExists(cityId))
			{
				return NotFound();
			}

			var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
			if (pointOfInterestEntity == null)
			{
				return NotFound();
			}

			Mapper.Map(pointOfInterest, pointOfInterestEntity);

			if (!_cityInfoRepository.Save())
			{
				return StatusCode(500, "A problem occured while handling your request.");
			}

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
			
			if (!_cityInfoRepository.CityExists(cityId))
			{
				return NotFound();
			}

			var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
			if (pointOfInterestEntity == null)
			{
				return NotFound();
			}

			var pointofInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

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

			Mapper.Map(pointofInterestToPatch, pointOfInterestEntity);

			if (!_cityInfoRepository.Save())
			{
				return StatusCode(500, "A problem occured while handling your request.");
			}

			return NoContent();
		}

		[HttpDelete("{cityId}/pointsofinterest/{id}")]
		public IActionResult DeletePointOfInterest(int cityId, int id)
		{
			if (!_cityInfoRepository.CityExists(cityId))
			{
				return NotFound();
			}

			var pointOfInterestEntity = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
			if (pointOfInterestEntity == null)
			{
				return NotFound();
			}

			_cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);

			if (!_cityInfoRepository.Save())
			{
				return StatusCode(500, "A problem occured while handling your request.");
			}

			_mailService.Send("Point of interest deleted.",
				$"Point of interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");

			return NoContent();
		}
	}
}