using HospitalManager.Communication.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalManager.Infrastructure.Entities
{
    public class AppointmentReturn
    {
        [Key]
        public Guid ReturnId { get; set; } = new Guid();
        public Guid DoctorId { get; set; }
        public virtual Doctor? Doctor { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public AppointmentStatus? Status { get; set; }
        public Guid PatientId { get; set; }
        public virtual Patient? Patient{ get; set; }
        [ForeignKey(nameof(AppointmentId))]
        public Guid AppointmentId { get; set; }
    }
}
