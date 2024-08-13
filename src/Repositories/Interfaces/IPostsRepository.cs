using System.Collections.Generic;
using System.Threading.Tasks;
using SocialClone.Models;

public interface IPostRepository
{
    Task<IEnumerable<Posts>> GetPostsByUserAsync(string userName);
    Task<Posts> createPostAsync(Posts post);
}