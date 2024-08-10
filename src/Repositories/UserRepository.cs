using SocialClone.DTO;
using SocialClone.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;


public class UserRepository : IUserRepository
{

     private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    
   
    
    public async Task<User> AddUserAsync(User user)
{
    // Add the user entity to the context
    await _context.Users.AddAsync(user);
    
    // Save changes to the database
    await _context.SaveChangesAsync();

    // Return the added user
    return user;
}


    public Task<bool> DeleteUserAsync(int userName)
    {
        throw new NotImplementedException();
    }

    

    public async Task<User> GetUserByNameAsync(string userName)
    {
        
        
        var user = await _context.Users
                                  .Where(u => u.UserName == userName)
                                  .SingleAsync();



        return user;

        
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        // Fetch all users from the database asynchronously
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetVerifiedUser(string userName, string password)
    {
        var user = await _context.Users.FindAsync(userName);

        if (user == null || !user.VerifyPassword(password))
        {
            throw new AuthenticationException("Invalid username or password");
        }

        return user;
    }
   
}