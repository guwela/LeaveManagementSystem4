namespace LeaveManagementSystem4.Application.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM : BaseLeaveTypeVM
    {

        public string Name { get; set; } = string.Empty;

        [Display(Name = "Days Allowed")]
        public int NumberOfDay { get; set; }
    }
}
