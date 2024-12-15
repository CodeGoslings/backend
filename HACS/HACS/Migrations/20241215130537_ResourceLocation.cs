using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace HACS.Migrations
{
    /// <inheritdoc />
    public partial class ResourceLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Sqlite:InitSpatialMetaData", true);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Resources",
                type: "POINT",
                nullable: false)
                .Annotation("Sqlite:Srid", 4326);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Resources");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Sqlite:InitSpatialMetaData", true);
        }
    }
}
