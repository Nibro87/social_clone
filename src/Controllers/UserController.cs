using SocialClone.Service;
using Microsoft.AspNetCore.Mvc;
using SocialClone.Models;
using SocialClone.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
namespace SocialClone.Controllers;

[ApiController]
[Route("api/[controller]")]     
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;
  
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("all")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            _logger.LogInformation("Fetched {Count} users.", users.Count());

            if (!users.Any())
            {
                return NoContent(); // 204 No Content
            }

            return Ok(users); // 200 OK with the list of users
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error"); // 500 Internal Server Error
        }
    }
    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is null.");
        }

        try
        {
            _userService.createUser(userDto);

            // Assuming the GetUserByName method exists and takes a username as a parameter.
            return CreatedAtAction("GetUserByName", new { username = userDto.UserName }, userDto);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here for debugging purposes
            return StatusCode(StatusCodes.Status500InternalServerError, "A problem happened while handling your request.");
        }

    
    }

[HttpPost("create-user")]
public async Task<IActionResult> CreateUserWithRoles([FromBody] UserDto userDto)
{
    if (userDto == null || string.IsNullOrEmpty(userDto.UserName) || string.IsNullOrEmpty(userDto.UserPass))
    {
        return BadRequest("Invalid user data.");
    }

    // Create a user entity from the provided UserDto
    var userDtoWithTimestamps = new UserDto
    {
        UserName = userDto.UserName,
        UserPass = userDto.UserPass,  // Assuming 'Password' is the correct property name
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    // Assuming the request has a property 'RoleNames' that contains the roles
    var user = await _userService.CreateUserWithRoleAsync(userDtoWithTimestamps, userDto.Roles);

    return CreatedAtAction("GetUserByName", new { userName = user.UserName }, user);
}


    [HttpPost("create-role")]
    public IActionResult CreateRole([FromBody] RoleDto roleDto)
    {
        if (roleDto == null)
        {
            return BadRequest("Role data is null.");
        }

        try
        {
        // Create the role with the provided roleDto
            _userService.CreateRoleAsync(roleDto);

        // Return a 201 Created status with no content
            return CreatedAtAction(nameof(CreateRole), new { }, null);
        }
        catch (Exception ex)
        {
        // Log the exception (ex) for debugging purposes
        // For example: _logger.LogError(ex, "An error occurred while creating the role.");
            return StatusCode(StatusCodes.Status500InternalServerError, "A problem occurred while handling your request.");
        }
}
    

    


}