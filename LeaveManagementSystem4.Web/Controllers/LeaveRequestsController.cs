using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem4.Web.Controllers;

[Authorize]

public class LeaveRequestsController(ILeaveTypesService _leaveTypesService, ILeaveRequestsService _leaveRequestsService) : Controller
{
    // GET: LeaveRequests
    public async Task<IActionResult> Index()
    {
        // This method retrieves all leave requests and returns them to the view.
        var model = await _leaveRequestsService.GetEmployeeLeaveRequest(); // Fetch all leave requests from the service
        return View(model);
    }

    public async Task<IActionResult> Create(int? leaveTypeId)
    {

        var leaveTypes = await _leaveTypesService.GetAll(); // Fetch all leave types from the service
        var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId); // Populate the SelectList with leave types
        var model = new LeaveRequestCreateVM
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1), // Default to one day leave
            LeaveTypes = leaveTypesList,
        };
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequestCreateVM model)
    {
        if (await _leaveRequestsService.RequestDatesExceedAllocation(model))
        {
            ModelState.AddModelError(string.Empty, "Number of days is Invaild");
            ModelState.AddModelError(nameof(model.EndDate), "Requested leave dates exceed available allocation.");
        }
        if (ModelState.IsValid)
        {
            await _leaveRequestsService.CreateLeaveRequest(model);
            return RedirectToAction(nameof(Index)); // Redirect to the index after successful creation
        }
        var leaveTypes = await _leaveTypesService.GetAll(); // Fetch all leave types from the service
        model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name"); // Populate the SelectList with leave types
        return View(model); // Replace with actual view model
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        await _leaveRequestsService.CancelLeaveRequest(id); // Call the service to cancel the leave request
        return RedirectToAction(nameof(Index)); // Redirect to the index after cancellation
    }

    [Authorize(Policy = "AdminSupervisorOnly")]
    public async Task<IActionResult> ListRequests()
    {
        var model = await _leaveRequestsService.AdminGetEmployeeLeaveRequest(); // Fetch all leave requests for admin view
        return View(model); // Replace with actual view model
    }
    public async Task<IActionResult> Review(int id)
    {
        var model = await _leaveRequestsService.ReviewLeaveRequstForReview(id);
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int id, bool approved)
    {
        await _leaveRequestsService.ReviewLeaveRequest(id, approved);
        return RedirectToAction(nameof(ListRequests));
    }
}

