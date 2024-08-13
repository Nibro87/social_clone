using SocialClone.Service;
using Microsoft.AspNetCore.Mvc;
using SocialClone.Models;
using SocialClone.DTO;
using Microsoft.AspNetCore.Authorization;

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
    //[Authorize(Policy = "AdminOnly")]
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

    


}