using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SocialClone.Service;
using SocialClone.Models;

using System.Text.Json;
using System.Security.Authentication;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _config;
    
    private readonly int _tokenExpireMinutes = 1000 * 60 * 30;

    public AuthenticationController(UserService userService,IConfiguration config )
    {
        _userService = userService;
        _config = config;
    }
    
[HttpPost]
[Consumes("application/json")]
[Produces("application/json")]
public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
{
    if (loginRequest == null || string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.password))
    {
        return BadRequest(new { message = "Username and password are required" });
    }

    try
    {
        // Authenticate the user
        var user = await _userService.GetVerifiedUser(loginRequest.UserName, loginRequest.password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password! Please try again" });
        }

        // Generate JWT token
        var token = GenerateJSONWebToken(user);
        
        var response = new
        {
            username = loginRequest.UserName,
            token = token
        };

        return Ok(response);
    }
    catch (AuthenticationException)
    {
        return Unauthorized(new { message = "Invalid username or password! Please try again" });
    }
    catch (Exception)
    {
        return StatusCode(500, new { message = "Internal server error occurred" });
    }
}


private string GenerateJSONWebToken(User userInfo)
{
        // Create security key and signing credentials
#pragma warning disable CS8604 // Possible null reference argument.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
#pragma warning restore CS8604 // Possible null reference argument.
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    // Define claims
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
        
        // Add roles as claims
        new Claim(ClaimTypes.Role, string.Join(",", userInfo.Roles))
    };

    // Create the token
    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"], // Ensure you have this in your configuration
        claims: claims,
        expires: DateTime.Now.AddMinutes(_tokenExpireMinutes), // Adjust token expiration as needed
        signingCredentials: credentials);

    // Generate the token string
    return new JwtSecurityTokenHandler().WriteToken(token);
}



}



