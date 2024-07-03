using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Infrastructure;

namespace HospitalManager.Application.UseCases.Doctors.GetDocByCpf
{
    public class GetDoctorDocByCpfUseCase
    {
        public ResponseDoctorDocJson Execute(RequestDoctorDocJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Doctors.FirstOrDefault(doctor => doctor.CPF == request.CPF);

            var imgPath = entity.DocImg;

            Byte[] b = System.IO.File.ReadAllBytes(imgPath);

            return new ResponseDoctorDocJson
            {
                imgDoc = b
            };
        }
    }
}
