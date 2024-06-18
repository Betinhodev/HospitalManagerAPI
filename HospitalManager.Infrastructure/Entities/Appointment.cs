using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int DoctorId { get; set; }
        public DateTime RegisterDate { get; set; }
        public Enum? Status { get; set; }
        public virtual Doctor? Doctor { get; set; } 
        public int PatientId { get; set; }
        public virtual Patient? Patient { get; set; }
        public decimal Value { get; set; }
    }
}
