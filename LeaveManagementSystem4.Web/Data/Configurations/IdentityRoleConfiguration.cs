namespace LeaveManagementSystem4.Web.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                 new IdentityRole
                 {
                     Id = "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2", //sample GUID, replace with your own
                     Name = "Employee",
                     NormalizedName = "EMPLOYEE"
                 },
                 new IdentityRole
                 {
                     Id = "ed2586e1-9838-494c-8501-523a6abfa166",
                     Name = "Supervisor",
                     NormalizedName = "SUPERVISOR"
                 },
                 new IdentityRole
                 {
                     Id = "ba8c81aa-6405-4168-bfed-eb146450ab46",
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR"
                 });
        }
    }
}
