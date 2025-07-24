using AutoMapper;
using LeaveManagementSystem4.Web.Data;
using LeaveManagementSystem4.Web.Models.LeaveTypes;

namespace LeaveManagementSystem4.Web.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
        }  
    }
}
  