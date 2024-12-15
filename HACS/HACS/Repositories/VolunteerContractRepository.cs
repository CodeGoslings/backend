using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Interfaces;
using HACS.Models;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories
{
    public class VolunteerContractRepository : IVolunteerContractRepository
    {
        private readonly ApplicationDBContext _context;
        public VolunteerContractRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VolunteerContract>> GetByVolunteer(int volunteerId)
        {
            return await _context.VolunteerContracts.Include(x => x.Organization).Where(a => a.VolunteerId == volunteerId).ToListAsync();
        }
    }
}