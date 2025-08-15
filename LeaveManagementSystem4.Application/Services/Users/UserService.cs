using Microsoft.AspNetCore.Http;

namespace LeaveManagementSystem4.Application.Services.Users
{
    public class UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor _httpContextAccessor) : IUserService
    {
        public async Task<ApplicationUser> GetLoggedInUser()
        {
            var user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            return user;
        }
        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user;
        }
        public async Task<List<ApplicationUser>> GetEmployee()
        {
            var users = await userManager.GetUsersInRoleAsync(Roles.Employee);
            return users.ToList();
        }
    }
}
