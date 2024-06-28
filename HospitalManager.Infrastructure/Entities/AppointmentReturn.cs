using HospitalManager.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class AppointmentReturn
    {
        [Key]
        public Guid ReturnId { get; set; } = new Guid();
        public Guid DoctorId { get; set; }
        public Doctor Doctor{ get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus? Status { get; set; }
        public Guid PatientId { get; set; }
        public Patient? Patient{ get; set; }
        public Guid AppointmentId { get; set; }
    }
}
