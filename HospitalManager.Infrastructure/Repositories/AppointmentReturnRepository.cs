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
    public class AppointmentReturnRepository : IAppointmentReturnRepository
    {
        private readonly HospitalManagerDbContext _context;

        public AppointmentReturnRepository(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public void Add(AppointmentReturn appointmentReturn)
        {
            _context.AppointmentReturns.Add(appointmentReturn);
            _context.SaveChanges();
        }
    }
}
