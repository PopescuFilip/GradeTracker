using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradeTrackerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemovedJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Subjects_SubjectEntityId",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "ClassSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SubjectEntityId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "SubjectEntityId",
                table: "Classes");

            migrationBuilder.CreateTable(
                name: "ClassEntitySubjectEntity",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "int", nullable: false),
                    SubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassEntitySubjectEntity", x => new { x.ClassesId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_ClassEntitySubjectEntity_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassEntitySubjectEntity_Subjects_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassEntitySubjectEntity_SubjectsId",
                table: "ClassEntitySubjectEntity",
                column: "SubjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassEntitySubjectEntity");

            migrationBuilder.AddColumn<int>(
                name: "SubjectEntityId",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClassSubjects",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSubjects", x => new { x.ClassId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ClassSubjects_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectEntityId",
                table: "Classes",
                column: "SubjectEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSubjects_SubjectId",
                table: "ClassSubjects",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Subjects_SubjectEntityId",
                table: "Classes",
                column: "SubjectEntityId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
