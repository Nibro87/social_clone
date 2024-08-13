using System;
namespace SocialClone.DTO;
public class PostsDTO
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required string CreatorUserName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }




}