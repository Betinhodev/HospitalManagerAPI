using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalManagerDbContext _context;

        public DoctorRepository(HospitalManagerDbContext context)
        {
            _context = context;
        }

        public void Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public Doctor GetByCpf(string doctorCpf)
        {
            return _context.Doctors.FirstOrDefault(doctor => doctor.CPF == doctorCpf);
        }

        public Doctor GetById(Guid doctorId)
        {
            return _context.Doctors.FirstOrDefault(doctor => doctor.DoctorId == doctorId);
        }


        public string GetDoctorName(Guid doctorId)
        {
            return _context.Doctors.Where(d => d.DoctorId == doctorId).Select(d => d.Name).FirstOrDefault();
        }

        public List<Appointment> GetRelatedAppointments(RequestDoctorRelatedAppointmentsJson request)
        {
            var doctorId = _context.Doctors.Where(d => d.CPF == request.CPF).Select(d => d.DoctorId).FirstOrDefault();
            return _context.Appointments.Where(appointment => appointment.DoctorId == doctorId).Where(appointment => appointment.Status == request.Status).ToList();
        }

        public void Validate(RequestDoctorJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            if (dbContext.Doctors.Any(doctor => doctor.CPF == request.CPF))
            {
                throw new ErrorOnValidationException("The CPF already exists in the database");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Name field can't be null.");
            }
            if (string.IsNullOrWhiteSpace(request.CPF))
            {
                throw new ErrorOnValidationException("CPF field can't be null.");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ErrorOnValidationException("Password field can't be null.");
            }
        }
    }
}
