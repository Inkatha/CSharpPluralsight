using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.Controllers
{
	public class TestController : Controller
	{
		private CityInfoContext _context;
		
		public TestController(CityInfoContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Route("api/testdatabase")]
		public IActionResult TestDatabase()
		{
			return Ok();
		}
	}
}
