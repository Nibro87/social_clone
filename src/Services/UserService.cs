namespace SocialClone.Service;

using System.Collections.Generic;
using SocialClone.DTO;
using SocialClone.Service;
using SocialClone.Models;
using System.Security.Authentication;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async void createUser(UserDto userDto)
    {
        var user = new User(userDto);
        
        _userRepository.AddUserAsync(user);
    }

    public async Task CreateRoleAsync(RoleDto roleDto)
    {
        var role = new Role(roleDto);
        await _userRepository.AddRoleAsync(role); // Await the asynchronous call
    }

    public void DeleteUser()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllUsersAsync();
        
        if (!users.Any())
        {
            throw new InvalidOperationException("The database does not contain any users!");
        }

        return users.Select(user => new UserDto(user));
    }

    public UserDto GetUserByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetVerifiedUser(string userName, string password)
    {
        return _userRepository.GetVerifiedUser(userName, password);
    }


    public void UpDateUser()
    {
        throw new NotImplementedException();
    }


}
