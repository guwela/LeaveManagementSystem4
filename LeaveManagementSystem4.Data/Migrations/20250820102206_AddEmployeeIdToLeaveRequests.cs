using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem4.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeIdToLeaveRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveDocuments",
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
                    table.PrimaryKey("PK_LeaveDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveDocuments_LeaveRequests_LeaveRequestId",
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
                values: new object[] { "102abd15-6aa2-405d-9c08-b671327128e8", "AQAAAAIAAYagAAAAECx9Aga/rVz39Z4+fHdXhVqHMKJNk6+X5Z2BLTl/1Zq/XDkoFajyLwCkaHuTpllEqw==", "23347ab8-4b33-45e3-8076-7954de6a96e8" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveDocuments_LeaveRequestId",
                table: "LeaveDocuments",
                column: "LeaveRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveDocuments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a870ae16-96eb-48f6-bb0c-43fe09593c28",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c2f3093-c5de-4dda-9acf-dcc66b8ebf20", "AQAAAAIAAYagAAAAEFyrUd9GhhBTWI2lALQGbsD2tXsQI2MPiyyB5ufHLun+AiVKU61zlilDEzNoxbYrfw==", "7155c678-c115-4cf3-add9-7621b1dc9300" });
        }
    }
}
