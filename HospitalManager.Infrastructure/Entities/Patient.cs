using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Entities
{
    public class Patient
    {
        [Key]
        public Guid PatientId { get; set; } = Guid.NewGuid();
        public string CPF { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string DocImg { get; set; } = string.Empty;
        public bool HasCovenant { get; set; }
        [ForeignKey(nameof(CovenantId))]
        public Guid CovenantId { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<AppointmentReturn> Returns { get; set; }

    }
}
