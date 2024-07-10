using HospitalManager.Communication.Enums;

namespace HospitalManager.Communication.Requests.Doctor
{
    public class RequestDoctorRelatedAppointmentsJson
    {
        public string CPF { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }

    }
}
