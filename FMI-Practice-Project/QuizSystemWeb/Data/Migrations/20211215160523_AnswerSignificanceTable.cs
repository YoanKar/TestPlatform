using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizSystemWeb.Data.Migrations
{
    public partial class AnswerSignificanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "IsCorrectId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnswerSignificances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerSignificances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_IsCorrectId",
                table: "Answers",
                column: "IsCorrectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AnswerSignificances_IsCorrectId",
                table: "Answers",
                column: "IsCorrectId",
                principalTable: "AnswerSignificances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AnswerSignificances_IsCorrectId",
                table: "Answers");

            migrationBuilder.DropTable(
                name: "AnswerSignificances");

            migrationBuilder.DropIndex(
                name: "IX_Answers_IsCorrectId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "IsCorrectId",
                table: "Answers");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
