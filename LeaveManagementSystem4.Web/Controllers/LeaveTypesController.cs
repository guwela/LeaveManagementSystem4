namespace LeaveManagementSystem4.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]// Ensure that only users with the Administrator role can access this controller
    public class LeaveTypesController(ILeaveTypesService _leaveTypeService) : Controller
    {



        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            // This method retrieves all leave types and returns them to the view.
            var viewData = await _leaveTypeService.GetAll();
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the leave type details using the service
            var leaveType = await _leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        [HttpGet]
        public IActionResult CreateModal()
        {// Initialize a new instance of the view model for creating a leave type
            var vm = new LeaveTypeCreateVM(); 
            // This returns a partial view for creating a new leave type
            return PartialView("_CreateForm", vm); 
        }
        // Check if the request is an AJAX request
        private bool IsAjaxRequest()
        {
            return Request.Headers["X-Requested-With"] == "XMLHttpRequest"; 
        }


        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            if (await _leaveTypeService.CheckIfLeaveTypeNameExists(leaveTypeCreate.Name))
            {
                ModelState.AddModelError("Name", "Leave type with this name already exists.");
            }

            if (!ModelState.IsValid)
            {
                if (IsAjaxRequest())
                    return PartialView("_CreateForm", leaveTypeCreate); // re-render errors in modal
                return View(leaveTypeCreate);
            }

            await _leaveTypeService.Create(leaveTypeCreate);

            if (IsAjaxRequest())
            {
                return Json(new { ok = true }); // return a JSON response indicating success
            }

            return RedirectToAction(nameof(Index)); // redirect to the index action after successful creation
        }


        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Fetch the leave type details using the service
            var leaveType = await _leaveTypeService.Get<LeaveTypeEditVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            // adding custom validation and model state error
            if (await _leaveTypeService.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), "Leave type with this name already exists.");
            }
            // ModelState.AddModelError(nameof(leaveTypeEdit.Name), "This is a custom error message for Name field.");
            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypeService.Edit(leaveTypeEdit);
                }
                // Handle concurrency issues
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypeService.LeaveTypeExists(leaveTypeEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEdit);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // Fetch the leave type details using the service
            var leaveType = await _leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypeService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
