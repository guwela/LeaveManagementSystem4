

namespace LeaveManagementSystem4.Web.Services.Users
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetEmployee();
        Task<ApplicationUser> GetLoggedInUser();
        Task<ApplicationUser> GetUserById(string userId);
    }
}