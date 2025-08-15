namespace LeaveManagementSystem4.Application.Models.Periods;

public class PeriodVM
{
    public int Id { get; set; } // Unique identifier for the period

    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
