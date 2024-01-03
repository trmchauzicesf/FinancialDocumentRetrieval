using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialDocumentRetrieval.DAL.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "985ee0e6-cf07-4628-b6e5-b136aaba6364", "b4c3bc0c-aeda-4ace-8b5d-1aa83d348d96", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5e4418e-f1ff-4a16-ab91-d75a87c4ab52", "aa65d1bc-a7f2-4528-8f94-b123ee932c59", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "985ee0e6-cf07-4628-b6e5-b136aaba6364");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5e4418e-f1ff-4a16-ab91-d75a87c4ab52");
        }
    }
}
