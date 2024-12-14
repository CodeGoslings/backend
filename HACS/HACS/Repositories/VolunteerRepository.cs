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
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDBContext _context;
        public VolunteerRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<Volunteer?> CreateAsync(Volunteer volunteer)
        {
            throw new NotImplementedException();
        }

        public Task<Volunteer?> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Volunteer>> GetAllAsync()
        {
            return await _context.Volunteers.Include(x => x.Assignments).ToListAsync();
        }

        public async Task<Volunteer?> GetByIdAsync(int id)
        {
            var volunteer = await _context.Volunteers.Include(x => x.Assignments).FirstOrDefaultAsync(x => x.Id == id);

            if (volunteer == null)
            {
                return null;
            }

            return volunteer;
        }

        public Task<Volunteer?> UpdateAsync(int id, Volunteer volunteer)
        {
            throw new NotImplementedException();
        }
    }
}