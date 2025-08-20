using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem4.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveRequestDocumentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequestDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveRequestId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequestDocuments_LeaveRequests_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalTable: "LeaveRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c2f3093-c5de-4dda-9acf-dcc66b8ebf20", "AQAAAAIAAYagAAAAEFyrUd9GhhBTWI2lALQGbsD2tXsQI2MPiyyB5ufHLun+AiVKU61zlilDEzNoxbYrfw==", "7155c678-c115-4cf3-add9-7621b1dc9300" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestDocuments_LeaveRequestId",
                table: "LeaveRequestDocuments",
                column: "LeaveRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequestDocuments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "abe5f069-5e27-42d3-967f-d4939fd2f622", "AQAAAAIAAYagAAAAEOJobT6p7GBfSyWl1b90im+/YIvn/1xsDXjpjIMz+rPnld/oNdGIDNjvCSFQJqQJhQ==", "fd42a4b5-e23e-4d7d-8a8e-4e224886d1b3" });
        }
    }
}
