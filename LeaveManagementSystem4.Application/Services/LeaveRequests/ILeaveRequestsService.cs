using LeaveManagementSystem4.Data.Models;

namespace LeaveManagementSystem4.Application.Services.LeaveRequests
{
    public interface ILeaveRequestsService // Interface for Leave Requests Service
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model); // Method to create a leave request
        Task<EmployeeLeaveRequestListVM> AdminGetEmployeeLeaveRequest(); // Method to get employee leave requests
        Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequest(); // Method to get all leave requests
        Task CancelLeaveRequest(int leaveRequestId); // Method to cancel a leave request
        Task ReviewLeaveRequest(int leaveRequestId, bool approved); // Method to review a leave request
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model); // Method to check if requested dates exceed allocation
        Task<ReviewLeaveRequestVM> ReviewLeaveRequstForReview(int id);

        Task<LeaveRequestDocument?> GetDocumentById(int id); // Method to get a leave request document by ID
        // Removed duplicate Task<LeaveDocument?> GetDocumentById(int id);
    }
}
