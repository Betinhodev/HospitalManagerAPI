
using HospitalManager.Communication.Requests.Appointment;

namespace HospitalManager.Communication.Responses.Doctor
{
    public class ResponseRelatedDoctorAppointments
    {
        public List<RequestAppointmentJson> Appointments { get; set; }
    }
}
