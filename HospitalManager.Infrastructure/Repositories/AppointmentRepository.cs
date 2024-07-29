using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;

namespace HospitalManager.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalManagerDbContext _context;

        public AppointmentRepository(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public void Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }

        public Appointment GetById(Guid appointmentId)
        {
            return _context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
        }
    }
}
