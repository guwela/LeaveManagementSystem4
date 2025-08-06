namespace LeaveManagementSystem4.Web.Data
{
    public class Period : BaseEntity
    {
      
        public String Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}