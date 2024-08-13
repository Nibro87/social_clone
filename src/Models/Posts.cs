using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SocialClone.DTO;
namespace SocialClone.Models;

    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}


        [StringLength(250, MinimumLength = 1)]
        public string Content {get; set;} = string.Empty;

        public string CreatorUserName { get; set; } = string.Empty;
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        
        
        [ForeignKey(nameof(CreatorUserName))]
        public User Creator { get; set; }
        

        public Posts()
        {
        }

        public Posts(string content, string creatorUserName, User creator)
        {
        
        this.Content = content;
        CreatorUserName = creatorUserName;
        Creator = creator;
        }

    public override bool Equals(object? obj)
    {
        return obj is Posts posts &&
               Id == posts.Id &&
               Content == posts.Content &&
               CreatorUserName == posts.CreatorUserName &&
               EqualityComparer<User>.Default.Equals(Creator, posts.Creator);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Content, CreatorUserName, Creator);
    }
}