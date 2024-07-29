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
    }
}
