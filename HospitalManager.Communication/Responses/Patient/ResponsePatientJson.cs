namespace HospitalManager.Communication.Responses.Patient
{
    public class ResponsePatientJson
    {
        public Guid Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public bool HasCovenant { get; set; }
        public int CovenantId { get; set; }
        public int AppointmentId { get; set; }
    }
}
