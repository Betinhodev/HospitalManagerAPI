using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Repositories.Interfaces;

namespace HospitalManager.Application.UseCases.Doctors.GetDocByCpf
{
    public class GetDoctorDocByCpfUseCase
    {
        private readonly IDoctorRepository _doctorRepository;
        public GetDoctorDocByCpfUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public ResponseDoctorDocJson Execute(RequestDoctorDocJson request)
        {

            var entity = _doctorRepository.GetByCpf(request.CPF);

            var imgPath = entity.DocImg;

            Byte[] b = System.IO.File.ReadAllBytes(imgPath);

            return new ResponseDoctorDocJson
            {
                imgDoc = b
            };
        }
    }
}
