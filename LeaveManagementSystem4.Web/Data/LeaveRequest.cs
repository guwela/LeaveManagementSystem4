namespace LeaveManagementSystem4.Web.Data
{
    public class LeaveRequest : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LeaveType? LeaveType { get; set; } // Navigation property to LeaveType

        public int LeaveTypeId { get; set; } // Foreign key to LeaveType
        public LeaveRequestStatus? LeaveRequstStatus { get; set; } // Navigation property to LeaveRequstStatus
        public int LeaveRequstStatusId { get; set; } // Foreign key to LeaveRequstStatus

        public ApplicationUser? Employee { get; set; } // Navigation property to ApplicationUser

        public string EmployeeId { get; set; } = default!; // Foreign key to ApplicationUser

        public ApplicationUser? Reviewer { get; set; } // Navigation property to ApplicationUser for the reviewer

        public string? ReviewerId { get; set; } // Foreign key to ApplicationUser for the reviewer

        public string? RequestComments { get; set; } // Comment from the employee requesting leave
    }
}