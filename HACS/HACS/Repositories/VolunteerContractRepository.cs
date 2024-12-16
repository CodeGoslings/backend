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

        public async Task<VolunteerContract> CreateAsync(VolunteerContract contract)
        {
            await _context.VolunteerContracts.AddAsync(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<VolunteerContract?> DeleteAsync(int id)
        {
            var contract = await _context.VolunteerContracts.FirstOrDefaultAsync(x => x.Id == id);

            if (contract == null)
            {
                return null;
            }
            _context.VolunteerContracts.Remove(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<List<VolunteerContract>> GetAllAsync()
        {
            return await _context.VolunteerContracts.ToListAsync();
        }

        public async Task<VolunteerContract?> GetByIdAsync(int id)
        {
            return await _context.VolunteerContracts.FindAsync(id);
        }

        public async Task<IEnumerable<VolunteerContract>> GetByOrganization(int organizationId)
        {
            return await _context.VolunteerContracts.Include(x => x.Volunteer).Where(a => a.OrganizationId == organizationId).ToListAsync();
        }

        public async Task<IEnumerable<VolunteerContract>> GetByVolunteer(int volunteerId)
        {
            return await _context.VolunteerContracts.Include(x => x.Organization).Where(a => a.VolunteerId == volunteerId).ToListAsync();
        }

        public async Task<VolunteerContract?> UpdateAsync(int id, VolunteerContract contract)
        {
            var existingContract = await _context.VolunteerContracts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingContract == null)
            {
                return null;
            }

            existingContract.From = contract.From;
            existingContract.To = contract.To;

            await _context.SaveChangesAsync();
            return existingContract;
        }
    }
}