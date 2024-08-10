namespace SocialClone.Service;
using SocialClone.DTO;
using SocialClone.Models;
public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    UserDto GetUserByName(string name);
    Task<User>GetVerifiedUser(string name,string password);
    void createUser(UserDto userDto);
    void UpDateUser();
    void DeleteUser();
}