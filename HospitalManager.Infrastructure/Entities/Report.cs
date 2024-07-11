using HospitalManager.Communication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Entities
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public Guid AppointmentId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string DoctorName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ExamType ExamType { get; set; }
    }
}
