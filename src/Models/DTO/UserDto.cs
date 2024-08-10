using SocialClone.Models;
namespace SocialClone.DTO;
public class UserDto
{
    public string UserName { get; set; } = string.Empty;
    public string UserPass { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<string> Roles { get; set; } = new List<string>();

    public UserDto() { }

    public UserDto(string userName, string userPass, DateTime createdAt, DateTime updatedAt, List<string> roles)
    {
        UserName = userName;
        UserPass = userPass;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Roles = roles;
    }

    public UserDto(User user)
    {
        UserName = user.UserName;
        UserPass = user.UserPass;
        CreatedAt = user.CreatedAt;
        UpdatedAt = user.UpdatedAt;
        
    }



}