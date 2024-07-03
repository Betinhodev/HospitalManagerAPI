using Microsoft.AspNetCore.Http;

namespace HospitalManager.Communication.Requests.Patient
{
    public class RequestPatientJson
    {
        public string CPF { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public bool HasCovenant { get; set; }
        public IFormFile? imgDoc { get; set; }
        public Guid CovenantId { get; set; }

    }
}
