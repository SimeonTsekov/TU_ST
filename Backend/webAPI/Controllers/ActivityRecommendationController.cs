using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActivityRecommendationController : Controller
	{
		[HttpPost]
		public IActionResult Create()
		{
			throw new NotImplementedException();
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id)
		{
			throw new NotImplementedException();

		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			throw new NotImplementedException();

		}

		[HttpGet]
		public IActionResult GetAllActivityRecommendations()
		{
			throw new NotImplementedException();

		}

		[HttpGet("{id}")]
		public IActionResult GetActivityRecommendation(int id)
		{
			throw new NotImplementedException();

		}
	}
}
