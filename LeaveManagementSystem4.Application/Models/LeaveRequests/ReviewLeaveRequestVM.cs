namespace LeaveManagementSystem4.Application.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM Employee { get; set; } = new EmployeeListVM(); // Employee details associated with the leave request

        [Display(Name = "Request Comments")]
        public string RequestComments { get; set; }

        [Display(Name = "Leave Type")]
        public int? DocumentId { get; set; } // nullable if no document

        [Display(Name = "Document Name")]
        public string? DocumentName { get; set; }

    }
}