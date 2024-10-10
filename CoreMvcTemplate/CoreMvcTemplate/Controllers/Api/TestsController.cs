using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreMvcTemplate.Controllers.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestsController : ControllerBase
	{


		[HttpGet]
		[Authorize]
		public ActionResult<IEnumerable<string>> Get()
		{
			var testData = new List<string>
			{
				"Test1",
				"Test2",
				"Test3"
			};

			return Ok(testData);
		}

	}
}
