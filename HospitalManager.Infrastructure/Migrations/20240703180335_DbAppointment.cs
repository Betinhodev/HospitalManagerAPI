using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "AppointmentReturns");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "AppointmentReturns",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "AppointmentReturns");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "AppointmentReturns",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
