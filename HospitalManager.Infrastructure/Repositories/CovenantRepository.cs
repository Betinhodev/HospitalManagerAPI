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
    public class CovenantRepository : ICovenantRepository
    {
        private readonly HospitalManagerDbContext _context;

        public CovenantRepository(HospitalManagerDbContext context)
        {
            _context = context;
        }
        public void Add(Covenant covenant)
        {
            _context.Covenants.Add(covenant);
            _context.SaveChanges();
        }

        public Covenant GetById(Guid covenantId)
        {
            return _context.Covenants.FirstOrDefault(covenant => covenant.CovenantId == covenantId);
        }
    }
}
