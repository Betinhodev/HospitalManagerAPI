using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        string GetDoctorName(Guid doctorId);
        Doctor GetById(Guid doctorId); 
        Doctor GetByCpf(string doctorCpf);
        List<Appointment> GetRelatedAppointments(RequestDoctorRelatedAppointmentsJson request);
        void Add(Doctor doctor);
        void Validate(RequestDoctorJson request);
    }
}
