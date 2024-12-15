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
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDBContext _context;
        public ResourceRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Resource> CreateAsync(Resource resource)
        {
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<Resource?> DeleteAsync(int id)
        {
            var resource = await _context.Resources.FirstOrDefaultAsync(x => x.Id == id);

            if (resource == null)
            {
                return null;
            }
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<Resource?> GetByIdAsync(int id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task<IEnumerable<Resource>> GetByOrganization(int organizationId)
        {
            return await _context.Resources.Where(x => x.OrganizationId == organizationId).ToListAsync();
        }

        public async Task<Resource?> UpdateAsync(int id, Resource resource)
        {
            var existingResource = await _context.Resources.FirstOrDefaultAsync(x => x.Id == id);
            if (existingResource == null)
            {
                return null;
            }

            existingResource.Name = resource.Name;
            existingResource.Amount = resource.Amount;
            existingResource.Unit = resource.Unit;

            await _context.SaveChangesAsync();
            return existingResource;
        }
    }
}