using SocialClone.DTO;
using SocialClone.Models;
using SocialClone.Repositories;
using SocialClone.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialClone.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository,IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;

        }


        public async Task<Posts> CreatePostAsync(PostsDTO postDto)
        {
            var creator = await _userRepository.GetUserByNameAsync(postDto.CreatorUserName);
            if (creator ==null)
                throw new Exception("User not found");
            
            var post = new Posts
            {
                Content = postDto.Content,
                Creator = creator,
                CreatedAt = postDto.CreatedAt,
                UpdatedAt = postDto.UpdatedAt
            };

            return await _postRepository.createPostAsync(post);
        }

        public async Task<IEnumerable<Posts>> GetPostsByUserAsync(string userName)
        {
            return await _postRepository.GetPostsByUserAsync(userName);
        }
    }
}