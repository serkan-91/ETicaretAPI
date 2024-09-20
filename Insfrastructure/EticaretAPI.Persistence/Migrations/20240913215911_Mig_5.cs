using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EticaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StorageTypes",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "StorageTypes", table: "Files");
        }
    }
}
