namespace LeaveManagementSystem4.Application.Models.LeaveAllocations;

public class EmployeeListVM
{

    public string Id { get; set; } // Unique identifier for the employee    

    [Display(Name = "First Name")]
    public string FirstName { get; set; } // Employee's first name

    [Display(Name = "Last Name")]
    public string LastName { get; set; } // Employee's last name
    [Display(Name = "Email Address")]
    public string Email { get; set; } // Employee's email address
}

