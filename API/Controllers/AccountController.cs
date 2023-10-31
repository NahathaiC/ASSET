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
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Position = user.Position,
                Department = user.Department,
                Section = user.Section,
                Phone = user.Phone,
                Status = user.Status,
                Token = await _tokenService.GenerateToken(user)

            };
        }

        // [HttpPost("register")]
        // public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        // {
        //     var user = new User
        //     {
        //         UserName = registerDto.Name,
        //         Id = registerDto.Id,
        //         Email = registerDto.Email,
        //         Position = registerDto.Position,
        //         Department = registerDto.Department,
        //         Section = registerDto.Section,
        //         Phone = registerDto.Phone
        //     };

        //     var result = await _userManager.CreateAsync(user, registerDto.Password);

        //     if (!result.Succeeded)
        //     {
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError(error.Code, error.Description);
        //         }

        //         return ValidationProblem();
        //     }

        //     // Assign the "EMP" role to the new user
        //     await _userManager.AddToRoleAsync(user, "EMP");

        //     return StatusCode(201);
        // }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
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

                await _userManager.AddToRoleAsync(user, "EMP");

                return StatusCode(201);
            }
            catch (Exception)
            {
                // Log the exception details for further investigation
                // Example: _logger.LogError(ex, "An error occurred while registering a user.");

                // Return an error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateRoles/{userId}")]
        public async Task<ActionResult> UpdateRoles(string userId, [FromQuery] string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound(); // User not found
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            var rolesToAdd = roles.Except(existingRoles).ToArray();
            var rolesToDelete = existingRoles.Except(roles).ToArray();

            var resultAdd = await _userManager.AddToRolesAsync(user, rolesToAdd);
            var resultDelete = await _userManager.RemoveFromRolesAsync(user, rolesToDelete);

            if (resultAdd.Succeeded && resultDelete.Succeeded)
            {
                return Ok(); // Roles updated successfully
            }
            else
            {
                // Handle errors
                foreach (var error in resultAdd.Errors.Concat(resultDelete.Errors))
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
        }


        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Position = user.Position,
                Department = user.Department,
                Section = user.Section,
                Phone = user.Phone,
                Status = user.Status,
                Token = await _tokenService.GenerateToken(user)
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return NoContent();

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete user.");
        }


    }
}