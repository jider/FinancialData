using findata_api.DTOs.Account;
using findata_api.interfaces;
using findata_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace findata_api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = userManager.Users.FirstOrDefault(user => user.UserName.ToLower() == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid credentials!");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid credentials!");

            return Ok(new NewUserDto {
                Username = user.UserName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);
                if (!createdUser.Succeeded) return BadRequest(createdUser.Errors);
                
                var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

                return Ok(new NewUserDto {
                    Email = appUser.Email,
                    Username = appUser.UserName,
                    Token = tokenService.CreateToken(appUser)
                });
            }
            catch (Exception error)
            {
                return StatusCode(500, error);
                throw;
            }
        }
    }
}
