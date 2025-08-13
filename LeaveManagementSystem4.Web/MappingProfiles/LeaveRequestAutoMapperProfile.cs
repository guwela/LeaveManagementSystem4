using AutoMapper;
using LeaveManagementSystem4.Web.Models.LeaveRequests;

namespace LeaveManagementSystem4.Web.MappingProfiles
{
    public class LeaveRequestAutoMapperProfile : Profile
    {
        public LeaveRequestAutoMapperProfile()
        {
            CreateMap<LeaveRequestCreateVM, LeaveRequest>();   
        }
    }
}
