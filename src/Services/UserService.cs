namespace SocialClone.Service;

using System.Collections.Generic;
using SocialClone.DTO;
using SocialClone.Service;
using SocialClone.Models;
using System.Security.Authentication;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
     private readonly IRoleRepository _roleRepository;
    public UserService(IUserRepository userRepository,IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }
    
    public async void createUser(UserDto userDto)
    {
        var user = new User(userDto);
        
        await _userRepository.AddUserAsync(user);
    }

    public async Task CreateRoleAsync(RoleDto roleDto)
    {
        var role = new Role(roleDto);
        await _userRepository.AddRoleAsync(role); // Await the asynchronous call
    }

    public async Task<User> CreateUserWithRoleAsync(UserDto userDto, IEnumerable<string> roleNames)
        {
            // Create the user
            var user = new User(userDto);

            // Get roles from the role repository
            var roles = new List<Role>();
            foreach (var roleName in roleNames)
            {
                var role = await _roleRepository.GetRoleByNameAsync(roleName);
                if (role != null)
                {
                    roles.Add(role);
                }
                else
                {
                    // Optionally handle cases where the role does not exist
                    // For simplicity, you might want to add logic to create new roles
                }
            }

            // Assign roles to the user
            user.Roles = roles;

            // Save the user to the repository
            await _userRepository.AddUserAsync(user);

            return user;
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
