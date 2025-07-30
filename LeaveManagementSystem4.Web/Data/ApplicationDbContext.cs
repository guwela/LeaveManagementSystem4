using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem4.Web.Data
{
    // ApplicationDbContext class that inherits from IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor that accepts DbContextOptions
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2", //sample GUID, replace with your own
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new IdentityRole
                {
                    Id = "ed2586e1-9838-494c-8501-523a6abfa166",
                    Name = "Supervisor",
                    NormalizedName = "SUPERVISOR"
                },
                new IdentityRole
                {
                    Id = "ba8c81aa-6405-4168-bfed-eb146450ab46",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                });
            var hasher = new PasswordHasher<ApplicationUser>();
            // Seed the database with an admin user
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {

                    Id = "a870ae16-96eb-48f6-bb0c-43fe09593c28", //sample GUID, replace with your own
                    Email = "admin@localhost.com",
                    NormalizedEmail = "ADMIN@LOCALHOST.COM",
                    NormalizedUserName = "ADMIN@LOCALHOST.COM",
                    UserName = "admin@localhost.com",
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    EmailConfirmed = true,
                    FirstName = "Default",
                    LastName = "Admin",
                    DateOfBirth = new DateOnly(1990, 11, 01) // Example date of birth
                });
            // Seed the database with an additional admin role
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "a870ae16-96eb-48f6-bb0c-43fe09593c28", //sample GUID, replace with your own
                    RoleId = "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2" //sample GUID, replace with your own for admin
                });
        }
    
        
            // DbSet for LeaveType entity
            public DbSet<LeaveType> LeaveTypes  { get; set; }
        }
    }
