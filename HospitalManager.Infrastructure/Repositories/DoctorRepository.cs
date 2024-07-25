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
        public string GetDoctorName(Guid doctorId)
        {
            return _context.Doctors.Where(d => d.DoctorId == doctorId).Select(d => d.Name).FirstOrDefault();
        }
    }
}
