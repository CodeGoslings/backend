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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDBContext _context;
        public OrganizationRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Organization> CreateAsync(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<Organization?> DeleteAsync(int id)
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(x => x.Id == id);

            if (organization == null)
            {
                return null;
            }
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<List<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(int id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task<Organization?> UpdateAsync(int id, Organization organization)
        {
            var existingOrganization = await _context.Organizations.FirstOrDefaultAsync(x => x.Id == id);
            if (existingOrganization == null)
            {
                return null;
            }

            existingOrganization.Name = organization.Name;
            existingOrganization.Email = organization.Email;
            existingOrganization.Website = organization.Website;
            existingOrganization.VatIN = organization.VatIN;

            await _context.SaveChangesAsync();
            return existingOrganization;
        }
    }
}