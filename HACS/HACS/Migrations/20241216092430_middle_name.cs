using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HACS.Migrations
{
    /// <inheritdoc />
    public partial class middle_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Users",
                newName: "MiddleName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Users",
                newName: "SecondName");
        }
    }
}
