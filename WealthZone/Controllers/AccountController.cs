using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WealthZone.Data.Interface;
using WealthZone.Dto.Account;
using WealthZone.Models;

namespace WealthZone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        public AccountController(SignInManager<ApplicationUser> _signInManager, ITokenService _tokenService, UserManager<ApplicationUser> _userManager)
        {
            this.userManager = _userManager;
            this.tokenService = _tokenService;
            this.signInManager = _signInManager;
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
            if (user == null) { return Unauthorized("Invalid Username"); }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password incorrect");
            }
            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email =user.Email,
                    Token = tokenService.CreateToken(user)


                }
            );
        }
            [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    return BadRequest(ModelState);
                }
                var appUser = new ApplicationUser
                {
                    Email = registerDto.Email,
                    UserName = registerDto.Username,
                };
                var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        //return Ok("User created");
                        return Ok(
                            new NewUserDto
                            {
                                 Username = appUser.UserName,
                                 Email = appUser.Email,
                                 Token = tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
