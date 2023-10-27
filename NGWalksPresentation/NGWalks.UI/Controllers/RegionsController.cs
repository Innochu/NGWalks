using Microsoft.AspNetCore.Mvc;
using NGWalks.UI.Models.Dto;

namespace NGWalks.UI.Controllers
{
	public class RegionsController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public RegionsController(IHttpClientFactory httpClientFactory)
        {
			_httpClientFactory = httpClientFactory;
		}
        public async Task<IActionResult> Index()
		{
			List<RegionDto> response = new List<RegionDto>();
			//get all regions fro web api
			try
			{
				var client = _httpClientFactory.CreateClient();
				var httpResponceMessage = await client.GetAsync("https://localhost:7293/api/region");

				httpResponceMessage.EnsureSuccessStatusCode();

				 response.AddRange(await httpResponceMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());

				
			}
			catch (Exception ex)
			{

				throw;
			}
			return View(response);
		}
	}
}
