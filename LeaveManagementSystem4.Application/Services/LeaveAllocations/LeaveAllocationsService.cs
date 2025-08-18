using AutoMapper;


namespace LeaveManagementSystem4.Application.Services.LeaveAllocations;

public class LeaveAllocationsService(ApplicationDbContext _context, IUserService _userService, IMapper _mapper, IPeriodsService _periodsService)
    : ILeaveAllocationsService
{
    public async Task AllocateLeave(string employeeId) 
    {
        // get all the leave types
        var leaveTypes = await _context.LeaveTypes
            .Where(q => !q.LeaveAllocations.Any(x => x.EmployeeId == employeeId))
            .ToListAsync();

        // get the current period based on the year
        var period = await _periodsService.GetCurrentPeriod(); // Get the current period

        var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

        // foreach leave type, create an allocation entry
        foreach (var leaveType in leaveTypes)
        {
           
            var accuralRate = decimal.Divide(leaveType.NumberOfDay, 12);
            var leaveAllocation = new LeaveAllocation
            {
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
            };

            _context.Add(leaveAllocation);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId) 
    {
        // Get the user by ID or the logged-in user if no ID is provided
        var user = string.IsNullOrEmpty(userId)
            ? await _userService.GetLoggedInUser()
            : await _userService.GetUserById(userId); 

        var allocations = await GetAllocations(user.Id);
        var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
        var leaveTypesCount = await _context.LeaveTypes.CountAsync();

        var employeeVm = new EmployeeAllocationVM // Create a new EmployeeAllocationVM instance
        {
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Id = user.Id,
            LeaveAllocations = allocationVmList,
            IsCompletedAllocation = leaveTypesCount == allocations.Count // Check if the number of leave types matches the number of allocations
        };

        return employeeVm;
    }

    public async Task<List<EmployeeListVM>> GetEmployees() // Get a list of employees
    {
        var users = await _userService.GetEmployee();
        // Map ApplicationUser to EmployeeListVM
        var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList()); 

        return employees;
    }

    public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
    {
        var allocation = await _context.LeaveAllocations // Fetch the leave allocation by ID
               .Include(q => q.LeaveType)
               .Include(q => q.Employee)
               .FirstOrDefaultAsync(q => q.Id == allocationId); 

        var model = _mapper.Map<LeaveAllocationEditVM>(allocation); // Map the allocation to LeaveAllocationEditVM

        return model;
    }

    public async Task EditAllocation(LeaveAllocationEditVM allocationEditVm) // Edit an existing leave allocation
    {

        // Update the Days property of the allocation
        await _context.LeaveAllocations
            .Where(q => q.Id == allocationEditVm.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVm.Days)); 
    }

    public async Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId)
    {
        // Fetch the current allocation for the specified leave type and employee
        var period = await _periodsService.GetCurrentPeriod();
        var allocation = await _context.LeaveAllocations 
            .FirstAsync(x => x.EmployeeId == employeeId
                                      && x.LeaveTypeId == leaveTypeId
                                      && x.PeriodId == period.Id); 
        return allocation;
    }

    private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
    {
        var period = await _periodsService.GetCurrentPeriod();
        var leaveAllocations = await _context.LeaveAllocations
           .Include(x => x.LeaveType)
           .Include(x => x.Period)
           .Where(x => x.EmployeeId == userId && x.Period.Id == period.Id)
           .ToListAsync(); // Fetch all leave allocations for the specified user in the current period
        return leaveAllocations;
    }

    private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
    {
        var exists = await _context.LeaveAllocations.AnyAsync(q =>
            q.EmployeeId == userId
            && q.LeaveTypeId == leaveTypeId
            && q.PeriodId == periodId
        );

        return exists;
    }


}