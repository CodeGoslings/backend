using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HACS.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerContractDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerContracts",
                table: "VolunteerContracts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VolunteerContracts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                table: "VolunteerContracts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                table: "VolunteerContracts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerContracts",
                table: "VolunteerContracts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerContracts_VolunteerId",
                table: "VolunteerContracts",
                column: "VolunteerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerContracts",
                table: "VolunteerContracts");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerContracts_VolunteerId",
                table: "VolunteerContracts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VolunteerContracts");

            migrationBuilder.DropColumn(
                name: "From",
                table: "VolunteerContracts");

            migrationBuilder.DropColumn(
                name: "To",
                table: "VolunteerContracts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerContracts",
                table: "VolunteerContracts",
                columns: new[] { "VolunteerId", "OrganizationId" });
        }
    }
}
