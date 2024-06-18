using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CovenantId",
                table: "Covenants",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Covenants",
                newName: "CovenantId");
        }
    }
}
