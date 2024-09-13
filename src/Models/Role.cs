using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SocialClone.DTO;

namespace SocialClone.Models
{
    public class Role
    {
        [Key]
        [Column("role_name", TypeName = "varchar(50)")]
        public string RoleName { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        // Many-to-Many Relationship
        [InverseProperty("Roles")]
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();

        // Parameterless constructor for EF
        public Role() { }

        // Constructor to initialize RoleName
        public Role(string roleName)
        {
            RoleName = roleName;
            CreatedAt = DateTime.UtcNow; // Initialize CreatedAt with the current date and time
            UpdatedAt = DateTime.UtcNow;
        }

        public Role(RoleDto roleDto)
        {
            RoleName = roleDto.RoleName;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"RoleName: {RoleName}, CreatedAt: {CreatedAt},UpdatedAt: {UpdatedAt}";
        }

       
    }

}
