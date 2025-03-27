using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class MadeTeacherDependOnSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SubjectId",
                table: "Teachers",
                column: "SubjectId",
                unique: true,
                filter: "[SubjectId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Subjects_SubjectId",
                table: "Teachers",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Subjects_SubjectId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SubjectId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Teachers_TeacherId",
                table: "Subjects",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
