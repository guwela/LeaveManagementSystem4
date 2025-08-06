using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem4.Web.Data
{
    public class LeaveType : BaseEntity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public int NumberOfDay { get; set; }

        public List<LeaveAllocation>? LeaveAllocations { get; set; }
    }
}
