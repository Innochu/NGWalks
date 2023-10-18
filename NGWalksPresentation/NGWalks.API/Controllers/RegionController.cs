using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGWalksDomain.Models;
using NGWalksPersistence;

namespace NGWalks.Presentation.Controllers
{
	//https://localhost:7293/api/region
	[Route("api/[controller]")]
	[ApiController]
	public class RegionController : ControllerBase

	{
		private readonly NGDbContext _nGDbContext;

		public RegionController(NGDbContext nGDbContext)
        {
			_nGDbContext = nGDbContext;
		}



		// Get : https://localhost:7293/api/Region

        [HttpGet]
		public IActionResult GetAll()
		{
			var regions = _nGDbContext.Regions.ToList();

			return Ok(regions);
		}
	}
}
