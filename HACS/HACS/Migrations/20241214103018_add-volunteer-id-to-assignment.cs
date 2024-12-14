using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HACS.Migrations
{
    /// <inheritdoc />
    public partial class addvolunteeridtoassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Volunteers_VolunteerId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "VolunteerId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Volunteers_VolunteerId",
                table: "Assignments",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Volunteers_VolunteerId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "VolunteerId",
                table: "Assignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Volunteers_VolunteerId",
                table: "Assignments",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id");
        }
    }
}
