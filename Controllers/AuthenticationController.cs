using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WalksAPI.Models.DTOs.Authentication;
using WalksAPI.Models.DTOs.AuthenticationDTO;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthenticationController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequest.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequest.Roles);
                    if (identityResult.Succeeded) 
                    {
                        return Ok(new { Message = "User registered successfully with roles." });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            Message = "User created but role assignment failed.",
                            Errors = identityResult.Errors.Select(e => e.Description)
                        });
                    }
                }
                return Ok(new { Message = "User registered successfully without roles." });
            }

            return BadRequest(new
            {
                Message = "User registration failed.",
                Errors = identityResult.Errors.Select(e => e.Description)
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await userManager.FindByEmailAsync(request.UserName);
            if (user != null)
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
                if (checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    // Remove the null check - GetRolesAsync never returns null, it returns empty list
                    var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

                    var response = new
                    {
                        JwtToken = jwtToken,
                        Email = user.Email,
                        Roles = roles.ToList() // Include roles in response for debugging
                    };
                    return Ok(new { Message = "Login successful.", response });
                }
            }
            return BadRequest(new { Message = "Invalid username or password." });
        }
    }
}