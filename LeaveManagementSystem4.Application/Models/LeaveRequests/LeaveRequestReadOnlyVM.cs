namespace LeaveManagementSystem4.Application.Models.LeaveRequests
{
    public class LeaveRequestReadOnlyVM
    {
        public int Id { get; set; } // Unique identifier for the leave request

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; } // Start date of the leave request

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; } // End date of the leave request

        [Display(Name = "Number of Days")]
        public int NumberOfDays { get; set; } // Total number of days requested

        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; } = string.Empty; // Type of leave requested (e.g., vacation, sick leave)

        [Display(Name = "Leave Status")]
        public LeaveRequestStatusEnum LeaveRequestStatus { get; set; } // Status of the leave request (e.g., Pending, Approved, Rejected)

        // Add this property for the uploaded document
        [Display(Name = "Document ID")]
        public int? DocumentId { get; set; }

        [Display(Name = "Employee First Name")]
        public string EmployeeFirstName { get; set; }

        [Display(Name = "Employee Last Name")]
        public string EmployeeLastName { get; set; }



    }
}