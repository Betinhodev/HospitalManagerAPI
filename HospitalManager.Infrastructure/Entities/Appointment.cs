using HospitalManager.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; } = Guid.NewGuid();
        public DateTime RegisterDate { get; set; }
        public AppointmentStatus? Status { get; set; }
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public decimal Value { get; set; }
    }
}
