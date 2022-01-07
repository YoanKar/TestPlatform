using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizSystemWeb.Data.Migrations
{
    public partial class AddIsCkeckedPropertyInResultsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Results",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Results");
        }
    }
}
