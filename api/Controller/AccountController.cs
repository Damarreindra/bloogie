using System;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signinManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;


        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (!createdUser.Succeeded)
            {
                return StatusCode(500, createdUser.Errors);
            }

            var addRoles = await _userManager.AddToRoleAsync(appUser, "User");
            if (!addRoles.Succeeded)
            {
                return StatusCode(500, addRoles.Errors);
            }

            return Ok(new NewUserDto
            {
                Username = appUser.UserName,
                Email = appUser.Email
            });
        }

       [HttpPost("login")]
       public async Task<IActionResult> Login([FromBody] LoginDto loginDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);
        if(user == null){
            return BadRequest(new{message = "Invalid Username Mate"});
        }

        var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if(!result.Succeeded){
            return BadRequest(new{message = "Invalid Password"});
        }
        return Ok(
            new NewUserDto{
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            }
        );
       }
    }
}