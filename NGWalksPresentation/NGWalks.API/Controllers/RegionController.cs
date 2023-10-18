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
		[Route("{id:Guid}")]
		public IActionResult Get([FromRoute] Guid id)
		{
			var region = _nGDbContext.Regions.FirstOrDefault(x => x.Id == id);
			if (region == null)
			{
				return NotFound();
			}


			//map region domain model into DTO
			var regionDTO = new RegionDTO()
			{
				Id = region.Id,
				Code = region.Code,
				Name = region.Name,
				RegionImageUrl = region.RegionImageUrl,
			};



			return Ok(regionDTO);
		}


		//Create Region from body
		//create : https://localhost:7293/api/Region/{id}

		[HttpPost]
		//[Route ("{Id:Guid}")]
		public IActionResult post( [FromBody] CreateRegionDTO createRegionDTO)
		{


			//map  DTO to domain model
			var add = new Region
			{
				Code = createRegionDTO.Code,
				Name = createRegionDTO.Name,
				RegionImageUrl = createRegionDTO.RegionImageUrl

			};
			
			_nGDbContext.Regions.Add(add);
			_nGDbContext.SaveChanges();

			//map model to DTO
			var add2 = new CreateRegionDTO()
			{
				
				Code = add.Code,
				Name = add.Name,
				RegionImageUrl = add.RegionImageUrl,

			};
			
			return Ok(add2);
		}

		//Update Region by id
		// Update : https://localhost:7293/api/Region/{id}
		[HttpPut]
		[Route("{id:Guid}")]
		public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
		{

			var update1 = _nGDbContext.Regions.FirstOrDefault(item => item.Id == id);
			if (update1 == null)
			{
				return NotFound();
			}
			//map DTO to Domian
			
			update1.Code = updateRegionDTO.Code;
			update1.Name = updateRegionDTO.Name;
			update1.RegionImageUrl = updateRegionDTO.RegionImageUrl;

			_nGDbContext.SaveChanges();

			//convert domain model to DTO model
			var update2 = new UpdateRegionDTO
			{
				//Id = update1.Id,
				Code = update1.Code,
				Name = update1.Name,
				RegionImageUrl = update1.RegionImageUrl,
			};
			return Ok(update2);
		}

		//delete Region by Id
		//Delete :  https://localhost:7293/api/Region/{id}

		[HttpDelete]
		[Route ("{id:Guid}")]

		public IActionResult Delete([FromRoute] Guid id)
		{
			var del = _nGDbContext.Regions.FirstOrDefault(item => item.Id == id);

            if (del == null)
				return NotFound();
           
			_nGDbContext.Regions.Remove(del);
			_nGDbContext.SaveChanges();
			return Ok();
        }
	}
}
