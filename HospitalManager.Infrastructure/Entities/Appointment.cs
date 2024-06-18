using HospitalManager.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DoctorId { get; set; }
        public DateTime RegisterDate { get; set; }
        public AppointmentStatus? Status { get; set; }
        public virtual Doctor? Doctor { get; set; } 
        public Guid PatientId { get; set; }
        public virtual Patient? Patient { get; set; }
        public decimal Value { get; set; }
    }
}
