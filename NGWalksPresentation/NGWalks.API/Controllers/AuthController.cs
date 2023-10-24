using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NGWalksDomain.ModelDTO;

namespace NGWalks.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;

		public AuthController(UserManager<IdentityUser> userManager)
        {
			_userManager = userManager;
		}




        //POST: /api/Auth/Register
        [HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
		{
			var identityUser = new IdentityUser
			{
				UserName = registerRequestDTO.UserName,
				Email = registerRequestDTO.UserName
			};

		var identityresult = 	await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);


			if(identityresult.Succeeded)
			{
				//add roles to this user
				if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
				{
					
					identityresult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

					if(identityresult.Succeeded)
					{
						return Ok("Registration Successful, please login");
					}

				}
				
			}
			return BadRequest("Unsuccessful Registeration");
		}
	}
}
