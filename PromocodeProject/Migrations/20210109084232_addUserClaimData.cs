using Microsoft.EntityFrameworkCore.Migrations;

namespace PromoCodeProject.Migrations
{
    public partial class addUserClaimData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "UserName" },
                values: new object[] { "Waseem" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetUsers",
               "UserName",
                ""
            );
        }
    }
}
