using SocialClone.Models;
using SocialClone.DTO;
public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByName(string roleName);
    Task<Role> AddRoleAsync(Role role);
    Task<Role> UpdateRoleAsync(Role role);
    Task<bool> DeleteRoleAsync(string roleName); 
}