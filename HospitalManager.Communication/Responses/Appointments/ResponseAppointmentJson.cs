
using HospitalManager.Communication.Enums;

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
