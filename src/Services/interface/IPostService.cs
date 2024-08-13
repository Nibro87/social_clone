using SocialClone.DTO;
using SocialClone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialClone.Services
{
    public interface IPostService
    {
        Task<Posts> CreatePostAsync(PostsDTO postDto);
        Task<IEnumerable<Posts>> GetPostsByUserAsync(string userName);
    }
}
