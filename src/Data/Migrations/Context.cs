using Microsoft.EntityFrameworkCore;

namespace SocialClone.Models;

// Define the DbContext
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

     // Property for DbSet of User entities
        public DbSet<User> Users { get; set; } = null!;

        // Property for DbSet of Role entities
        public DbSet<Role> Roles { get; set; } = null!;
        

        // Optionally override OnModelCreating to configure your models
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .HasMany(u => u.Roles)  // A User has many Roles
        .WithMany(r => r.Users) // A Role has many Users
        .UsingEntity<Dictionary<string, object>>(
            "UserRoles", // Name of the join table
            j => j.HasOne<Role>() // Configuration for Role entity
                  .WithMany() // Role has many UserRoles
                  .HasForeignKey("RoleName"), // Foreign key in UserRoles table
            j => j.HasOne<User>() // Configuration for User entity
                  .WithMany() // User has many UserRoles
                  .HasForeignKey("UserName") // Foreign key in UserRoles table
        );
}
    }
