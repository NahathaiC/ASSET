using API.DTOs.UserDtos;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<Role> _roleManager;
        public AccountController(UserManager<User> userManager, TokenService tokenService, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromForm] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto, [FromQuery] string[] roles)
        {
            var user = new User
            {
                UserName = registerDto.Name,
                Id = registerDto.Id,
                Email = registerDto.Email,
                Position = registerDto.Position,
                Department = registerDto.Department,
                Section = registerDto.Section,
                Phone = registerDto.Phone
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            return StatusCode(201);
        }



        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }


        // [Authorize(Roles = "Admin")]
        // [HttpPost("registerApp")]
        // public async Task<ActionResult> RegisterApp(RegisterDto registerDto)
        // {
        //     var user = new User{UserName = registerDto.Name, Id = registerDto.Id,Email = registerDto.Email,
        //                          Position = registerDto.Position, Department = registerDto.Department, Section = registerDto.Section,
        //                          Phone = registerDto.Phone};
        //     var result = await _userManager.CreateAsync(user, registerDto.Password);

        //     if (!result.Succeeded)
        //     {
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError(error.Code, error.Description);
        //         }

        //         return ValidationProblem();
        //     }

        //     await _userManager.AddToRoleAsync(user, "Approver");

        //     return StatusCode(201);
        // }
    }
}