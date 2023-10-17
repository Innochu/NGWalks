using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NGWalks.Presentation.Controllers
{
	//https://localhost:7293/api/Students
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		//GET: https://localhost:7293/api/Students
		[HttpGet]
		public IActionResult GetAllStudent()
		{
			string[] studentnames = new string[] { "John", "Jane", "Mark", "Emily", "David" };

			return Ok(studentnames);
		}
	}
}
