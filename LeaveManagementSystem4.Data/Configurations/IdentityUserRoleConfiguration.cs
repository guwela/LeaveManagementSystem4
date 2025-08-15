namespace LeaveManagementSystem4.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = "a870ae16-96eb-48f6-bb0c-43fe09593c28", //sample GUID, replace with your own
                    RoleId = "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2" //sample GUID, replace with your own for admin
                });
        }
    }
}
