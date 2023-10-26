using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NGWalksApplication;
using NGWalksDomain.ModelDTO;
using NGWalksDomain.Models;
using NGWalksDTOValidations;
using System.Text.Json;

namespace NGWalks.Presentation.Controllers
{
	//https://localhost:7293/api/region
	[Route("api/[controller]")]
	[ApiController]
	
	public class RegionController : ControllerBase

	{
		
		private readonly IRegionRepo _iRegionRepo;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public RegionController(IRegionRepo iRegionRepo, IMapper mapper, ILogger<RegionController> logger )
		{
			
			_iRegionRepo = iRegionRepo;
			_mapper = mapper;
			_logger = logger;
		}



		// Get : https://localhost:7293/api/Region
		//GET ALL REGIONS
		[HttpGet]
		//[Authorize(Roles = "Reader")]
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
			[FromQuery] string? sortBy, [FromQuery] bool isAscending,
			[FromQuery] int pageNumber , [FromQuery] int pageSize )
		{
			
				throw new Exception("This is a custom exception");

				//_logger.LogInformation("GetAll Region action method was invoked");
				//get data from database
				var regions = await _iRegionRepo.GetRegionsAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

				_logger.LogInformation($"Finished GetAllRegion Request with data: {JsonSerializer.Serialize(regions)}");
				//this will convert the data into jason data
				return Ok(_mapper.Map<List<RegionDTO>>(regions));
			
			
		}




		// Get : https://localhost:7293/api/Region/{id}
		//GET REGION BY ID
		[HttpGet]
		[Route("{Id:Guid}")]
		//[Authorize(Roles = "Reader")]
		public async Task<IActionResult> Get([FromRoute] Guid Id)
		{
			try
			{
				var region = await _iRegionRepo.GetRegionByIdAsync(Id);
				if (region == null)

					return NotFound();

				return Ok(_mapper.Map<RegionDTO>(region));

			}
			catch (Exception)
			{

				throw;
			}
		}






		//Create Region from body
		//create : https://localhost:7293/api/Region/{id}

		[HttpPost]
		[ModelStateValidation]
		//[Authorize(Roles = "Writer")]
		//[Route ("{Id:Guid}")]
		public async Task<IActionResult> Post( [FromBody] CreateRegionDTO createRegionDTO)
		{
			var add = _mapper.Map<Region>(createRegionDTO);

		 await _iRegionRepo.CreateAsync(add);
			
			
			var	regionDTO = _mapper.Map<RegionDTO>(add);

			return CreatedAtAction(nameof(Get), new { id = regionDTO.Id}, regionDTO);
		}





		//Update Region by id
		// Update : https://localhost:7293/api/Region/{id}
		[HttpPut]
		[Route("{Id:Guid}")]
		[ModelStateValidation]
		//[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdateRegionDTO updateRegionDTO)
		{

			var update1 = _mapper.Map<Region>(updateRegionDTO);

			update1  = await _iRegionRepo.UpdateRegionAsync(Id, update1);

			if (update1 == null)
			
			return NotFound();
			
			return Ok(_mapper.Map<RegionDTO>(update1));


		}






		//delete Region by Id
		//Delete :  https://localhost:7293/api/Region/{id}

		[HttpDelete]
		[Route ("{Id:Guid}")]
		//[Authorize(Roles = "Writer")]
		public async Task<IActionResult> Delete([FromRoute] Guid Id)
		{
			var del = await _iRegionRepo.DeleteRegion(Id);
            if (del == null)
				return NotFound();
           
			return Ok(_mapper.Map<RegionDTO>(del));


        }
	}
}
