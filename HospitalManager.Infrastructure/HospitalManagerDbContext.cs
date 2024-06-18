using HospitalManager.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManager.Infrastructure
{
    public class HospitalManagerDbContext : DbContext
    {

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments{ get; set; }
        public DbSet<AppointmentReturn> AppointmentReturns{ get; set; }
        public DbSet<Covenant> Covenants{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= C:\\Users\\gsnogueira\\source\\repos\\HospitalManager.API\\Database.db");
        }

    }
}
