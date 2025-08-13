﻿
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem4.Web.Data.Configurations
{
    public class LeaveRequeststatusConfiguration : IEntityTypeConfiguration<LeaveRequestStatus>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestStatus> builder)
        {
            builder.HasData(
                new LeaveRequestStatus
                {
                    Id = 1,
                    Name = "Pending"
                },
                new LeaveRequestStatus
                {
                    Id = 2,
                    Name = "Approved"
                },
                new LeaveRequestStatus
                {
                    Id = 3,
                    Name = "Rejected"
                },
                new LeaveRequestStatus
                {
                    Id = 4,
                    Name = "Cancelled"
                }
            );
        }

    }
}
