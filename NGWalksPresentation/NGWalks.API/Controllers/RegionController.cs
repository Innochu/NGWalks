using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGWalksDomain.ModelDTO;
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
		//GET ALL REGIONS
        [HttpGet]
		public IActionResult GetAll()
		{
			//get data from database
			var regions = _nGDbContext.Regions.ToList();

			//map domain models to DTO
			var regionDTO = new List<RegionDTO>();
			foreach (var region in regions)
			{
				regionDTO.Add(new RegionDTO()
				{
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl,

				});
			}
			

			return Ok(regionDTO);
		}


		// Get : https://localhost:7293/api/Region/{id}
		//GET REGION BY ID
		[HttpGet]
		[Route ("{id:Guid}")]
		public IActionResult Get( [FromRoute] Guid id)
		{
			var region = _nGDbContext.Regions.FirstOrDefault(x => x.Id == id);
			if (region == null)
			{
				return NotFound();
			}


			//map region domain model into DTO
			var regionDTO = new RegionDTO()
			{
				Id =region.Id,
				Code = region.Code,
				Name = region.Name,
				RegionImageUrl = region.RegionImageUrl,
			};
            


            return Ok(regionDTO);
		}
	}
}
