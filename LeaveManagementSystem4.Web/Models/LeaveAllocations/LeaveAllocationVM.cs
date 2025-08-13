using LeaveManagementSystem4.Web.Models.LeaveTypes;
using LeaveManagementSystem4.Web.Services.LeaveTypes;

namespace LeaveManagementSystem4.Web.Models.LeaveAllocations
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; } // Unique identifier for the leave allocation
        [Display(Name = "Number of Days")]
        public int Days { get; set; } // Number of days allocated for the leave type

        [Display(Name = "Allocation Period")]
        public PeriodVM Period { get; set; } = new PeriodVM(); // Allocation period for the leave

        public LeaveTypeReadOnlyVM LeaveType { get; set; } = new LeaveTypeReadOnlyVM(); // Leave type details
    }
}
