using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class AppointmentReturn
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor{ get; set; }
        public DateTime Date { get; set; }
        public Enum? Status { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient{ get; set; }
        public Guid AppointmentId { get; set; }
    }
}
