namespace LeaveManagementSystem4.Data.Models
{
    public class LeaveRequestDocument
    {
        public int Id { get; set; }
        public int LeaveRequestId { get; set; }
        public string DocumentName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }

        public LeaveRequest LeaveRequest { get; set; }
    }
}
