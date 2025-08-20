using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeaveManagementSystem4.Application.Services.LeaveRequests;
using LeaveManagementSystem4.Application.Services.LeaveTypes;
using LeaveManagementSystem4.Data.Models;

namespace LeaveManagementSystem4.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveTypesService _leaveTypesService;
        private readonly ILeaveRequestsService _leaveRequestsService;

        public LeaveRequestsController(ILeaveTypesService leaveTypesService, ILeaveRequestsService leaveRequestsService)
        {
            _leaveTypesService = leaveTypesService;
            _leaveRequestsService = leaveRequestsService;
        }

        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestsService.GetEmployeeLeaveRequest();
            return View(model);
        }

        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var leaveTypes = await _leaveTypesService.GetAll();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);

            var model = new LeaveRequestCreateVM
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
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
                ModelState.AddModelError(string.Empty, "Number of days is Invalid");
                ModelState.AddModelError(nameof(model.EndDate), "Requested leave dates exceed available allocation.");
            }

            if (ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index));
            }

            var leaveTypes = await _leaveTypesService.GetAll();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(model);
        }

        public async Task<IActionResult> DownloadDocument(int id)
        {
            var doc = await _leaveRequestsService.GetDocumentById(id);
            if (doc == null) return NotFound();

            return File(doc.FileContent, doc.ContentType, doc.DocumentName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await _leaveRequestsService.CancelLeaveRequest(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestsService.AdminGetEmployeeLeaveRequest();
            return View(model);
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
}
