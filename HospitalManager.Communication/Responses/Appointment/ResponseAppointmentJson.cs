using HospitalManager.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Communication.Responses.Appointment
{
    public class ResponseAppointmentJson
    {
        public string DoctorName { get; set; }
        public DateTime RegisterDate { get; set; }
        public string PatientName { get; set; }
        public AppointmentStatus? Status { get; set; } 
        public decimal Value { get; set; }
    }
}
