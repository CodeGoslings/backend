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
        public async Task<Volunteer?> CreateAsync(Volunteer volunteer)
        {
            await _context.Volunteers.AddAsync(volunteer);
            await _context.SaveChangesAsync();
            return volunteer;
        }

        public async Task<Volunteer?> DeleteAsync(int id)
        {
            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == id);

            if (volunteer == null)
            {
                return null;
            }
            _context.Volunteers.Remove(volunteer);
            await _context.SaveChangesAsync();
            return volunteer;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Volunteers.AnyAsync(x => x.Id == id);
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

        public async Task<Volunteer?> UpdateAsync(int id, Volunteer volunteer)
        {
            var existingVolunteer = await _context.Volunteers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingVolunteer == null)
            {
                return null;
            }

            existingVolunteer.FirstName = volunteer.FirstName;
            existingVolunteer.LastName = volunteer.LastName;
            existingVolunteer.Email = volunteer.Email;

            await _context.SaveChangesAsync();
            return existingVolunteer;
        }
    }
}