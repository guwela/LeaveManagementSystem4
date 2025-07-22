using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem4.Web.Data
{
    public class LeaveType
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        public int NumberOfDay { get; set; }
    }
}
