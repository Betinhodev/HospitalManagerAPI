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
        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source= C:\\Users\\gsnogueira\\source\\repos\\HospitalManager.API\\Database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(doctor => doctor.Doctor)
                .WithMany(a => a.Appointments)
                .HasForeignKey(doctor => doctor.DoctorId);

            modelBuilder.Entity<Appointment>()
                .HasOne(patient => patient.Patient)
                .WithMany(a => a.Appointments)
                .HasForeignKey(patient => patient.PatientId);

            modelBuilder.Entity<AppointmentReturn>()
                .HasOne(doctor => doctor.Doctor)
                .WithMany(a => a.Returns)
                .HasForeignKey(doctor => doctor.DoctorId);

            modelBuilder.Entity<AppointmentReturn>()
                .HasOne(patient => patient.Patient)
                .WithMany(a => a.Returns)
                .HasForeignKey(patient => patient.PatientId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
