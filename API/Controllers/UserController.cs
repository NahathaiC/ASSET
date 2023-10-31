using API.DTOs.UserDtos;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly RoleManager<Role> _roleManager;
        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager, TokenService tokenService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDtos = users.Select(async user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.GenerateToken(user),
                Position = user.Position,
                Department = user.Department,
                Section = user.Section,
                Phone = user.Phone,
                Status = user.Status
            }).Select(task => task.Result);

            return Ok(userDtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{emailOrId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUser(string emailOrId)
        {
            var users = await GetUsersByEmailOrId(emailOrId);

            var userDtos = users.Select(async user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
                Position = user.Position,
                Department = user.Department,
                Section = user.Section,
                Phone = user.Phone,
                Status = user.Status
            }).Select(task => task.Result);

            return Ok(userDtos);
        }

        private async Task<IEnumerable<User>> GetUsersByEmailOrId(string emailOrId)
        {
            List<User> users = new List<User>();

            if (int.TryParse(emailOrId, out int userId))
            {
                var user = await _userManager.FindByIdAsync(emailOrId);
                if (user != null)
                    users.Add(user);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(emailOrId);
                if (user != null)
                    users.Add(user);
            }

            return users;
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("updateUserStatus")]
        public async Task<IActionResult> UpdateUserStatus([FromBody] UpdateUserStatusDto updateUserStatusDto)
        {
            var user = await GetUserByEmailOrId(updateUserStatusDto.EmailOrId);
            if (user == null)
                return NotFound();

            // Update the user's status based on the provided input
            string status = updateUserStatusDto.Status?.ToUpper();
            if (status == "ACTIVE" || status == "RESIGN")
            {
                user.Status = status;
            }
            else
            {
                return BadRequest("Invalid status value. Valid values: 'Active', 'Resign'.");
            }

            // Save the changes to the database
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update user status.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            // Retrieve the user from the database
            var user = await _userManager.FindByIdAsync(userDto.Id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            // Update the User entity based on the provided UserDto
            user.Email = userDto.Email;
            user.UserName = userDto.UserName; // Set the UserName property
            user.Position = userDto.Position;
            user.Department = userDto.Department;
            user.Section = userDto.Section;
            user.Phone = userDto.Phone;

            // Save the changes to the database
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Return an updated UserDto without changing the status
                var updatedUserDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName, // Include UserName
                    Position = user.Position,
                    Department = user.Department,
                    Section = user.Section,
                    Phone = user.Phone,
                    // Don't include status in the response
                };

                return Ok(updatedUserDto);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update user.");
            }
        }

        private async Task<User> GetUserByEmailOrId(string emailOrId)
        {
            User user = null;
            if (int.TryParse(emailOrId, out int userId))
            {
                user = await _userManager.FindByIdAsync(emailOrId);
            }
            else
            {
                user = await _userManager.FindByEmailAsync(emailOrId);
            }

            return user;
        }
    }
}