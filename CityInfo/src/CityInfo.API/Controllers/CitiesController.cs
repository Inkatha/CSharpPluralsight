using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using CityInfo.API.Services;
using AutoMapper;

namespace CityInfo.API.Controllers
{
	[Route("api/cities")]
	public class CitiesController : Controller
	{
		private ICityInfoRepository _cityInfoRepository;

		public CitiesController(ICityInfoRepository cityInfoRepository)
		{
			_cityInfoRepository = cityInfoRepository;
		}

		[HttpGet()]
		public IActionResult GetCities()
		{
			var cityEntities = _cityInfoRepository.GetCities();

			var results = Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities);
			
			return Ok(results);
		}

		[HttpGet("{id}")]
		public IActionResult GetCity(int id, bool includePointsOfInterest = false)
		{
			var result = _cityInfoRepository.GetCity(id, includePointsOfInterest);
			if (result == null)
			{
				return NotFound();
			}

			if (includePointsOfInterest)
			{
				var cityResult = Mapper.Map<CityDto>(result);
				return Ok(cityResult);
			}

			var cityWithoutPointOfInterestResult = Mapper.Map<CityWithoutPointOfInterestDto>(result);

			return Ok(cityWithoutPointOfInterestResult);
		}
	}
}
