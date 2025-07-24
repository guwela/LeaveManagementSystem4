using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem4.Web.Models.LeaveTypes
{
    public class LeaveTypeCreateVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "Your lenght is invalid, please type again")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1,90)]
        [Display(Name = "Days Allowed")]
        public int NumberOfDay { get; set; }
    }
}
