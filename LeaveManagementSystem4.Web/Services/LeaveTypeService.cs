using AutoMapper;
using LeaveManagementSystem4.Web.Data;
using LeaveManagementSystem4.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem4.Web.Services;

// This class implements the ILeaveTypeService interface, providing methods to manage leave types. 
public class LeaveTypeService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeService
{
    

    public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
    {
        // This method retrieves all leave types from the database and maps them to a view model.
        var data = await _context.LeaveTypes.ToListAsync();

        // Convert the data model into view model using AutoMapper
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
        return viewData;
    }
    // This method retrieves a specific leave type by its ID and maps it to a view model.
    public async Task<T?> Get<T>(int id) where T : class
    {
        // Check if the ID is valid
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
        if (data == null)
        {
            return null;
        }
        // Convert the data model into view model using AutoMapper
        var viewData = _mapper.Map<T>(data);
        return viewData;
    }

    public async Task Remove(int id)
    {
        // This method removes a leave type from the database by its ID.
        var data = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
        if (data != null)
        {
            _context.LeaveTypes.Remove(data);
            await _context.SaveChangesAsync();
        }
    }

    // This method updates an existing leave type in the database.
    public async Task Edit(LeaveTypeEditVM model)
    {
        // Check if the leave type name already exists for another leave type
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.LeaveTypes.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    // This method creates a new leave type in the database.
    public async Task Create(LeaveTypeCreateVM model)
    {
        // Check if the leave type name already exists
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.LeaveTypes.Add(leaveType);
        await _context.SaveChangesAsync();
    }
    // GET: LeaveTypes/CheckIfLeaveTypeNameExists
    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        // Check if a leave type with the same name already exists
        var lowerCaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(e => e.Name.ToLower() == lowerCaseName);
    }

    // Check if leave type name exists for edit
    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
    {
        // Check if a leave type with the same name already exists, excluding the current leave type being edited
        var lowerCaseName = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(e => e.Name.ToLower() == lowerCaseName && e.Id != leaveTypeEdit.Id);
    }
}

