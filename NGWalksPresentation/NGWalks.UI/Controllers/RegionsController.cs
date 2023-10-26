using Microsoft.AspNetCore.Mvc;

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
			//get all regions fro web api
			try
			{
				var client = _httpClientFactory.CreateClient();
				var httpResponceMessage = await client.GetAsync("https://localhost:7293/api/region");

				httpResponceMessage.EnsureSuccessStatusCode();

				var stringResponceBody = await httpResponceMessage.Content.ReadAsStringAsync();

				ViewBag.Response = stringResponceBody;
			}
			catch (Exception ex)
			{

				throw;
			}
			return View();
		}
	}
}
