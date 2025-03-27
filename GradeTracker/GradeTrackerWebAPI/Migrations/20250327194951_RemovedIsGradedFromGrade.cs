using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedIsGradedFromGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGraded",
                table: "Grades");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGraded",
                table: "Grades",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
