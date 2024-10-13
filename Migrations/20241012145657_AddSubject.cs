using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subj_Code = table.Column<string>(nullable: false),
                    SubjectDescription = table.Column<string>(nullable: false),
                    Units = table.Column<int>(nullable: false),
                    Offering = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    Course_Code = table.Column<string>(nullable: false),
                    CurriculumYear = table.Column<int>(nullable: false),
                    SubjectRequisite = table.Column<string>(nullable: true),
                    IsPreRequisite = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
