using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameToClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Classes");
        }
    }
}
