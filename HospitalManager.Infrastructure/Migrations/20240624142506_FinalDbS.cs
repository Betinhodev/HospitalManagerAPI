using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FinalDbS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Covenants",
                table: "Covenants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Covenants");

            migrationBuilder.AddColumn<Guid>(
                name: "CovenantId",
                table: "Covenants",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Covenants",
                table: "Covenants",
                column: "CovenantId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Covenants",
                table: "Covenants");

            migrationBuilder.DropColumn(
                name: "CovenantId",
                table: "Covenants");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Covenants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Covenants",
                table: "Covenants",
                column: "Id");
        }
    }
}
