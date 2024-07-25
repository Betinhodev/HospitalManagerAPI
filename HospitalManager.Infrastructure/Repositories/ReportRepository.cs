using HospitalManager.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly HospitalManagerDbContext _context;

        public ReportRepository(HospitalManagerDbContext context)
        {
            _context = context;
        }

        public void Add(Report report)
        {
            _context.Reports.Add(report);
            _context.SaveChanges();  
        }
    }
}
