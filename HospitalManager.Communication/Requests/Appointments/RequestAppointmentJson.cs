namespace HospitalManager.Communication.Requests.Appointment
{
    public class RequestAppointmentJson
    {
        public Guid DoctorId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Guid PatientId { get; set; }

    }
}
