using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NGWalksApplication;
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
		
		private readonly IRegionRepo _iRegionRepo;

		public RegionController(IRegionRepo iRegionRepo)
		{
			
			_iRegionRepo = iRegionRepo;
		}



		// Get : https://localhost:7293/api/Region
		//GET ALL REGIONS
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//get data from database
			var regions = await _iRegionRepo.GetRegionsAsync();

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
		public async Task<IActionResult> Get([FromRoute] Guid Id)
		{
			var region = await _iRegionRepo.GetRegionByIdAsync(Id); 
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
		public async Task<IActionResult> post( [FromBody] CreateRegionDTO createRegionDTO)
		{


			//map  DTO to domain model
			var add = new Region
			{
				Code = createRegionDTO.Code,
				Name = createRegionDTO.Name,
				RegionImageUrl = createRegionDTO.RegionImageUrl

			};
			
			await _iRegionRepo.CreateAsync(add);
			

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
		public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionDTO updateRegionDTO)
		{

			var update1 = await _iRegionRepo.UpdateRegionAsync(Id, updateRegionDTO);
			if (update1 == null)
			{
				return NotFound();
			}
			//map DTO to Domian
			
			update1.Code = updateRegionDTO.Code;
			update1.Name = updateRegionDTO.Name;
			update1.RegionImageUrl = updateRegionDTO.RegionImageUrl;

			

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

		public async Task<IActionResult> Delete([FromRoute] Guid Id)
		{
			var del = _iRegionRepo.DeleteRegion(Id);
            if (del == null)
				return NotFound();
           
			
			return Ok();
        }
	}
}
