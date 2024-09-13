using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SocialClone.DTO;
namespace SocialClone.Models
{           
    public class User
    {
        [Key]
        [Required(ErrorMessage ="Name is required")]
        [StringLength(60,ErrorMessage ="Name can not be longer than 60 characters")]
        [Column("user_name", TypeName = "varchar(60)")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(100,MinimumLength =8,ErrorMessage ="Password must be at least 8 characters")]
        [Column("user_pass")]
        public string UserPass { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        // Many-to-Many Relationship
        [InverseProperty("Users")]
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();

        public ICollection<Posts> Posts { get; set; } = new HashSet<Posts>();

        

        // Method to get roles as strings
        public List<string> GetRolesAsStrings()
        {
            return Roles.Select(role => role.RoleName).ToList();
        }

        public void AddRole (Role role)
        {
            Roles.Add(role);
        }

        // Example method to verify password
        public bool VerifyPassword(string password)
        {
            // Note: Use a proper password hashing library for real-world scenarios
            return BCrypt.Net.BCrypt.Verify(password, UserPass);
        }

        public User(){}
        // Constructor with password hashing
        public User(string userName, string userPass)
        {
            UserName = userName;
            UserPass = BCrypt.Net.BCrypt.HashPassword(userPass,BCrypt.Net.BCrypt.GenerateSalt());
            CreatedAt = DateTime.UtcNow; // Initialize CreatedAt with the current date and time
            UpdatedAt = DateTime.UtcNow;
        }

        public User(UserDto userDto)
        {
        UserName = userDto.UserName;
        UserPass = BCrypt.Net.BCrypt.HashPassword(userDto.UserPass, BCrypt.Net.BCrypt.GenerateSalt());
        CreatedAt = userDto.CreatedAt;
        UpdatedAt = userDto.UpdatedAt;
        
        }
        

        public override string ToString()
        {
            return $"User{{UserName='{UserName}', UserPass='{UserPass}', CreatedAt='{CreatedAt}', Roles={Roles}";
        }
    }
}
