using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DbAppointmentss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppointmentReturns_AppointmentId",
                table: "AppointmentReturns",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentReturns_Appointments_AppointmentId",
                table: "AppointmentReturns",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentReturns_Appointments_AppointmentId",
                table: "AppointmentReturns");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentReturns_AppointmentId",
                table: "AppointmentReturns");
        }
    }
}
