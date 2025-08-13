using LeaveManagementSystem4.Web.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            base.OnModelCreating(builder); // Call the base method to apply Identity configurations

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  //Apply all configurations from the current assembly

        }
    
        
            // DbSet for LeaveType entity
            public DbSet<LeaveType> LeaveTypes  { get; set; } // DbSet for LeaveType entity
            public DbSet<LeaveAllocation> LeaveAllocations { get; set; } // DbSet for LeaveAllocation entity
            public DbSet<Period> Periods { get; set; } // DbSet for Period entity
            public DbSet<LeaveRequestStatus> LeaveRequstStatuses { get; set; } // DbSet for LeaveRequstStatus entity
            public DbSet<LeaveRequest> LeaveRequests { get; set; } // DbSet for LeaveRequest entity

    }
}
