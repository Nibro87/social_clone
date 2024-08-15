using SocialClone.Models;
using SocialClone.DTO;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByNameAsync(string userName);
    Task<User> GetVerifiedUser(string userName, string password);
    Task<User> AddUserAsync(User user);
    Task <Role> AddRoleAsync(Role role);
    Task<User> UpdateUserAsync(User userDto);
    Task<bool> DeleteUserAsync(int userName);
}