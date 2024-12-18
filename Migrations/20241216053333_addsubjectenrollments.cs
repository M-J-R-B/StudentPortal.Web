using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal.Web.Migrations
{
    /// <inheritdoc />
    public partial class addsubjectenrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEnrollments_Schedules_ScheduleEdpCode",
                table: "SubjectEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_SubjectEnrollments_ScheduleEdpCode",
                table: "SubjectEnrollments");

            migrationBuilder.DropColumn(
                name: "ScheduleEdpCode",
                table: "SubjectEnrollments");

            migrationBuilder.AlterColumn<string>(
                name: "EdpCode",
                table: "SubjectEnrollments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentDate",
                table: "SubjectEnrollments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_SubjectEnrollments_EdpCode",
                table: "SubjectEnrollments",
                column: "EdpCode");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEnrollments_Schedules_EdpCode",
                table: "SubjectEnrollments",
                column: "EdpCode",
                principalTable: "Schedules",
                principalColumn: "EdpCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectEnrollments_Schedules_EdpCode",
                table: "SubjectEnrollments");

            migrationBuilder.DropIndex(
                name: "IX_SubjectEnrollments_EdpCode",
                table: "SubjectEnrollments");

            migrationBuilder.DropColumn(
                name: "EnrollmentDate",
                table: "SubjectEnrollments");

            migrationBuilder.AlterColumn<string>(
                name: "EdpCode",
                table: "SubjectEnrollments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ScheduleEdpCode",
                table: "SubjectEnrollments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectEnrollments_ScheduleEdpCode",
                table: "SubjectEnrollments",
                column: "ScheduleEdpCode");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectEnrollments_Schedules_ScheduleEdpCode",
                table: "SubjectEnrollments",
                column: "ScheduleEdpCode",
                principalTable: "Schedules",
                principalColumn: "EdpCode");
        }
    }
}
