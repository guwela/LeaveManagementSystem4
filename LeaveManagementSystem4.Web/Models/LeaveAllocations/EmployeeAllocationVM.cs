namespace LeaveManagementSystem4.Web.Models.LeaveAllocations;

public class EmployeeAllocationVM : EmployeeListVM
{
    [Display(Name = "Date of Birth")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
    public bool IsCompletedAllocation { get; set; } // Indicates if the allocation is complete for the employee
    public List<LeaveAllocationVM> LeaveAllocations { get; set; }
}