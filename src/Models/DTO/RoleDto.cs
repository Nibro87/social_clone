using System;
using System.Collections.Generic;
using System.Linq;
using SocialClone.Models;

namespace SocialClone.DTO
{
    public class RoleDto
    {
        public string RoleName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<string> Users { get; set; } = new List<string>();

        public RoleDto() { }

        public RoleDto(string roleName, DateTime createdAt, DateTime updatedAt, List<string> users)
        {
            RoleName = roleName;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Users = users;
        }

        public RoleDto(Role role)
        {
            RoleName = role.RoleName;
            CreatedAt = role.CreatedAt;
            UpdatedAt = role.UpdatedAt;
            
        }
    }
}
