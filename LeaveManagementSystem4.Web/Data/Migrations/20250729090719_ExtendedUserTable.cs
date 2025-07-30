
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem4.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9001ccdf-bda4-4491-bcd9-416147fb1837", new DateOnly(1990, 11, 1), "Default", "Admin", "AQAAAAIAAYagAAAAEOnHleZ1cb2Ds2Kn1L9jw7KBK6+5ftuCfQI//aQkFSmLLFFksltKJL1WuWjTlxnBIA==", "ab2fc8cb-cd13-49db-adff-a238de71f4f4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df0ed379-5934-4504-899e-4717dd58288d", "AQAAAAIAAYagAAAAEEZW3OYjcmDIqLqDvss0Zi/Fc/KsEpjQkGxkEj+A0qJ38uG/LXXdcOFrPuEdsfO9nw==", "5a097efb-29b0-4385-b00e-094bd90b1b84" });
        }
    }
}
