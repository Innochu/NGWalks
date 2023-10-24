using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NGWalksApplication;
using NGWalksDomain.ModelDTO;
using NGWalksDomain.ModelDTO.LoginDTO;
using NGWalksValidations;
using LoginRequestDTO = NGWalksDomain.ModelDTO.LoginDTO.LoginRequestDTO;

namespace NGWalks.Presentation.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ITokenRepo _tokenRepo;

		public AuthController(UserManager<IdentityUser> userManager, ITokenRepo tokenRepo)
        {
			_userManager = userManager;
			_tokenRepo = tokenRepo;
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



		//Post : http/api/Auth/login
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
		{
			var user = await _userManager.FindByEmailAsync(loginRequestDTO.UserName);
			
			if (user != null)
			{
				var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

				if (checkPasswordResult)
				{
					var roles = await _userManager.GetRolesAsync(user);
					if(roles != null)
					{
					var jwtToken =	_tokenRepo.CreateJwtToken(user, roles.ToList());

						var response = new LoginResponseDTO
						{
							JwtToken = jwtToken,
						};
						return Ok(response);
					}
					
					
				}
				
			}

			return BadRequest("UserName or Password incorrect");
		}

	}
}
