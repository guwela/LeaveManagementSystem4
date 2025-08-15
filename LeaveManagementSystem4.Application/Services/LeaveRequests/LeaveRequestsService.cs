using AutoMapper;


namespace LeaveManagementSystem4.Application.Services.LeaveRequests;

public partial class LeaveRequestsService(IMapper _mapper,
    IUserService _userService,
    ApplicationDbContext _context,
    ILeaveAllocationsService _leaveAllocationsService) : ILeaveRequestsService
{
    public async Task<EmployeeLeaveRequestListVM> AdminGetEmployeeLeaveRequest()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType) // Include the LeaveType for each request
            .ToListAsync();

        var approvedRequestsCount = leaveRequests.Count(x => x.LeaveRequstStatusId == (int)LeaveRequestStatusEnum.Approved);
        var pendingRequestsCount = leaveRequests.Count(x => x.LeaveRequstStatusId == (int)LeaveRequestStatusEnum.Pending);
        var declinedRequestsCount = leaveRequests.Count(x => x.LeaveRequstStatusId == (int)LeaveRequestStatusEnum.Rejected);

        var leaveRequestsModels = leaveRequests.Select(x => new LeaveRequestReadOnlyVM
        {
            Id = x.Id,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            NumberOfDays = (x.EndDate.Date - x.StartDate.Date).Days,
            LeaveType = x.LeaveType.Name, // Assuming LeaveType has a Name property
            LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequstStatusId // Convert status ID to enum
        }).ToList();

        var model = new EmployeeLeaveRequestListVM
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = approvedRequestsCount,
            PendingRequests = pendingRequestsCount,
            RejectedRequests = declinedRequestsCount,
            LeaveRequests = leaveRequestsModels

        };
        return model; // Return the model containing the leave request data
    }

    public async Task CancelLeaveRequest(int leaveRequestId)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequstStatusId = (int)LeaveRequestStatusEnum.Cancelled;


        await UpdateAllocationDays(leaveRequest, false); // Restore the allocation days
        await _context.SaveChangesAsync();
    }

    public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model); // Map the ViewModel to the LeaveRequest entity

        var user = await _userService.GetLoggedInUser(); // Get the current user
        leaveRequest.EmployeeId = user.Id; // Set the EmployeeId from the current user

        // Set the initial status to Pending
        leaveRequest.LeaveRequstStatusId = (int)LeaveRequestStatusEnum.Pending;

        _context.Add(leaveRequest); // Add the leave request to the context

        await UpdateAllocationDays(leaveRequest, true); // Deduct the allocation days for the request
        await _context.SaveChangesAsync(); // Save changes to the database
    }
    public Task<LeaveRequestReadOnlyVM> GetAllLeaveRequests()
    {
        throw new NotImplementedException();
    }
    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
    {
        var user = await _userService.GetLoggedInUser(); // Get the current user

        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year); // Get the current period
        var numberOfDays = (model.EndDate.Date - model.StartDate.Date).Days; // Calculate the number of days requested
        var allocation = await _context.LeaveAllocations
            .FirstOrDefaultAsync(x => x.LeaveTypeId == model.LeaveTypeId
            && x.EmployeeId == user.Id
            && x.PeriodId == period.Id
            ); // Get the allocation for the user

        return allocation.Days < numberOfDays; // Check if the allocation is sufficient
    }

    public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
    {
        var user = await _userService.GetLoggedInUser(); // Get the current user
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequstStatusId = approved
            ? (int)LeaveRequestStatusEnum.Approved
            : (int)LeaveRequestStatusEnum.Rejected;

        leaveRequest.ReviewerId = user.Id;

        if (!approved)
        {
            await UpdateAllocationDays(leaveRequest, false); // Restore the allocation days if rejected

        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequest()
    {
        var user = await _userService.GetLoggedInUser(); // Get the current user

        var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType) // Include the LeaveType for each request
            .Where(x => x.EmployeeId == user.Id)
            .ToListAsync();


        var model = leaveRequests.Select(x => new LeaveRequestReadOnlyVM
        {
            Id = x.Id,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            NumberOfDays = (x.EndDate.Date - x.StartDate.Date).Days,
            LeaveType = x.LeaveType.Name, // Assuming LeaveType has a Name property
            LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequstStatusId // Convert status ID to enum
        }).ToList();

        return model;
    }

    public async Task<ReviewLeaveRequestVM> ReviewLeaveRequstForReview(int id)
    {
        var leaveRequst = await _context.LeaveRequests
            .Include(x => x.LeaveType) // Include the LeaveType for each request
            .FirstOrDefaultAsync(x => x.Id == id); // Find the leave request by ID

        var user = await _userService.GetUserById(leaveRequst.EmployeeId); // Get the current user

        var model = new ReviewLeaveRequestVM
        {
            StartDate = leaveRequst.StartDate,
            EndDate = leaveRequst.EndDate,
            NumberOfDays = (leaveRequst.EndDate.Date - leaveRequst.StartDate.Date).Days,
            LeaveType = leaveRequst.LeaveType.Name, // Assuming LeaveType has a Name property
            LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequst.LeaveRequstStatusId, // Convert status ID to enum
            RequestComments = leaveRequst.RequestComments,
            Id = leaveRequst.Id, // Set the ID of the leave request
            Employee = new EmployeeListVM
            {
                Id = leaveRequst.EmployeeId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }

        };
        return model;
    }


    private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
    {
        var allocation = await _leaveAllocationsService.GetCurrentAllocation(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
        var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);

        if (deductDays)
        {
            allocation.Days -= numberOfDays;
        }
        else
        {
            allocation.Days += numberOfDays;
        }
        _context.Entry(allocation).State = EntityState.Modified;
    }

    private int CalculateDays(DateTime start, DateTime end)
    {
        return (end - start).Days;
    }
}

