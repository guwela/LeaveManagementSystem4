using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem4.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // This migration seeds default roles and a user into the database.
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2", null, "Employee", "EMPLOYEE" },
                    { "a870ae16-96eb-48f6-bb0c-43fe09593c28", null, "Administrator", "ADMINISTRATOR" },
                    { "ed2586e1-9838-494c-8501-523a6abfa166", null, "Supervisor", "SUPERVISOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a870ae16-96eb-48f6-bb0c-43fe09593c28", 0, "df0ed379-5934-4504-899e-4717dd58288d", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEEZW3OYjcmDIqLqDvss0Zi/Fc/KsEpjQkGxkEj+A0qJ38uG/LXXdcOFrPuEdsfO9nw==", null, false, "5a097efb-29b0-4385-b00e-094bd90b1b84", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2", "a870ae16-96eb-48f6-bb0c-43fe09593c28" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed2586e1-9838-494c-8501-523a6abfa166");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2", "a870ae16-96eb-48f6-bb0c-43fe09593c28" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f9ebe35-6600-4145-9e63-56a4f8fa4bf2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28");
        }
    }
}
