using AutoMapper;
using LeaveManagementSystem4.Data.Models;
using LeaveManagementSystem4.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem4.Application.Services.LeaveRequests;

public partial class LeaveRequestsService(IMapper _mapper,
    IUserService _userService,
    ApplicationDbContext _context,
    ILeaveAllocationsService _leaveAllocationsService) : ILeaveRequestsService
{
    public async Task<EmployeeLeaveRequestListVM> AdminGetEmployeeLeaveRequest()
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType)
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
            LeaveType = x.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequstStatusId,
            DocumentId = _context.LeaveRequestDocuments
                 .Where(d => d.LeaveRequestId == x.Id)
                 .Select(d => (int?)d.Id)
                 .FirstOrDefault()
        }).ToList();


        var model = new EmployeeLeaveRequestListVM
        {
            TotalRequests = leaveRequests.Count,
            ApprovedRequests = approvedRequestsCount,
            PendingRequests = pendingRequestsCount,
            RejectedRequests = declinedRequestsCount,
            LeaveRequests = leaveRequestsModels
        };
        return model;
    }

    public async Task CancelLeaveRequest(int leaveRequestId)
    {
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequstStatusId = (int)LeaveRequestStatusEnum.Cancelled;

        await UpdateAllocationDays(leaveRequest, false);
        await _context.SaveChangesAsync();
    }

    public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
    {
        var user = await _userService.GetLoggedInUser(); // Get the logged-in user

        var leaveRequest = new LeaveRequest
        {
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            LeaveTypeId = model.LeaveTypeId,
            RequestComments = model.RequestComments,
            EmployeeId = user.Id, // Set the EmployeeId here!
            LeaveRequstStatusId = (int)LeaveRequestStatusEnum.Pending // Optional: set default status
        };

        _context.LeaveRequests.Add(leaveRequest);
        await _context.SaveChangesAsync();

        if (model.Document != null && model.Document.Length > 0)
        {
            using var ms = new MemoryStream();
            await model.Document.CopyToAsync(ms);

            var doc = new LeaveRequestDocument
            {
                LeaveRequestId = leaveRequest.Id,
                DocumentName = model.Document.FileName,
                ContentType = model.Document.ContentType,
                FileContent = ms.ToArray()
            };

            _context.LeaveRequestDocuments.Add(doc);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<LeaveRequestDocument?> GetDocumentById(int id)
    {
        return await _context.LeaveRequestDocuments.FirstOrDefaultAsync(d => d.Id == id);
    }

    public Task<LeaveRequestReadOnlyVM> GetAllLeaveRequests()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
    {
        var user = await _userService.GetLoggedInUser();

        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(x => x.EndDate.Year == currentDate.Year);
        var numberOfDays = (model.EndDate.Date - model.StartDate.Date).Days;
        var allocation = await _context.LeaveAllocations
            .FirstOrDefaultAsync(x => x.LeaveTypeId == model.LeaveTypeId
            && x.EmployeeId == user.Id
            && x.PeriodId == period.Id
            );

        return allocation.Days < numberOfDays;
    }

    public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
    {
        var user = await _userService.GetLoggedInUser();
        var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveRequstStatusId = approved
            ? (int)LeaveRequestStatusEnum.Approved
            : (int)LeaveRequestStatusEnum.Rejected;

        leaveRequest.ReviewerId = user.Id;

        if (!approved)
        {
            await UpdateAllocationDays(leaveRequest, false);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<LeaveRequestReadOnlyVM>> GetEmployeeLeaveRequest()
    {
        var user = await _userService.GetLoggedInUser();

        var leaveRequests = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .Where(x => x.EmployeeId == user.Id)
            .ToListAsync();

        var model = leaveRequests.Select(x => new LeaveRequestReadOnlyVM
        {
            Id = x.Id,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            NumberOfDays = (x.EndDate.Date - x.StartDate.Date).Days,
            LeaveType = x.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)x.LeaveRequstStatusId,
            DocumentId = _context.LeaveRequestDocuments
                 .Where(d => d.LeaveRequestId == x.Id)
                 .Select(d => (int?)d.Id)
                 .FirstOrDefault()
        }).ToList();


        return model;
    }

    public async Task<ReviewLeaveRequestVM> ReviewLeaveRequstForReview(int id)
    {
        var leaveRequst = await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);

        var user = await _userService.GetUserById(leaveRequst.EmployeeId);

        var model = new ReviewLeaveRequestVM
        {
            StartDate = leaveRequst.StartDate,
            EndDate = leaveRequst.EndDate,
            NumberOfDays = (leaveRequst.EndDate.Date - leaveRequst.StartDate.Date).Days,
            LeaveType = leaveRequst.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequst.LeaveRequstStatusId,
            RequestComments = leaveRequst.RequestComments,
            Id = leaveRequst.Id,
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
