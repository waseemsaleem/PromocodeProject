using Microsoft.EntityFrameworkCore.Migrations;

namespace PromoCodeProject.Migrations.PromoDb
{
    public partial class addTitleColumnInPromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PromoCodes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "PromoCodes");
        }
    }
}
