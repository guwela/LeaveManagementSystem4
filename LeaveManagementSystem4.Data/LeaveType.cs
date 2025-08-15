namespace LeaveManagementSystem4.Data
{
    public class LeaveType : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public int NumberOfDay { get; set; }

        public List<LeaveAllocation>? LeaveAllocations { get; set; }
    }
}
