using Microsoft.AspNetCore.Identity;

namespace LeaveManagementSystem4.Web.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
    
    }
}
