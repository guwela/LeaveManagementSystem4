namespace LeaveManagementSystem4.Web.Data
{
    public class LeaveRequestStatus : BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; } // Name of the status, e.g., "Pending", "Approved", "Rejected"

    }
}