using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Assignment;
using HACS.Models;

namespace HACS.Interfaces
{
    public interface IResourceRepository
    {
        Task<List<Resource>> GetAllAsync();
        Task<Resource?> GetByIdAsync(int id);
        Task<Resource> CreateAsync(Resource resource);
        Task<Resource?> UpdateAsync(int id, Resource resource);
        Task<Resource?> DeleteAsync(int id);
        Task<IEnumerable<Resource>> GetByOrganization(int organizationId);
    }
}