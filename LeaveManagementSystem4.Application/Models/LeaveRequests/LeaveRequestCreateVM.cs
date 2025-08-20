using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem4.Application.Models.LeaveRequests
{
    public class LeaveRequestCreateVM : IValidatableObject
    {
        [Display(Name = "Leave Start Date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "Leave End Date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "Leave Type")]
        [Required(ErrorMessage = "Please select a leave type.")]
        public int LeaveTypeId { get; set; } // Foreign key to LeaveType

        [Display(Name = "Additional Information")]
        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters.")]
        public string? RequestComments { get; set; } // Comment from the employee requesting leave

        public SelectList? LeaveTypes { get; set; } = default!; // SelectList for Leave Types

        // NEW: File upload
        [Display(Name = "Upload Document")]
        public IFormFile Document { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "The start date cannot be later than the end date.",
                    new[] { nameof(StartDate), nameof(EndDate) });
            }
        }
    }
}