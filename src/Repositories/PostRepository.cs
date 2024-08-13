using Microsoft.EntityFrameworkCore;
using SocialClone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialClone.Repositories
{
public class PostsRepository : IPostRepository
{

    private readonly ApplicationDbContext _context;

    public PostsRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Posts> createPostAsync(Posts post)
    {
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<IEnumerable<Posts>> GetPostsByUserAsync(string userName)
    {
        return await _context.Posts
            .Include(p => p.Creator)
            .Where(p => p.Creator.UserName == userName)
            .ToListAsync();
    }
}
}