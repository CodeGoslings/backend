using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Assignment;
using HACS.Models;

namespace HACS.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<List<Organization>> GetAllAsync();
        Task<Organization?> GetByIdAsync(int id);
        Task<Organization> CreateAsync(Organization organization);
        Task<Organization?> UpdateAsync(int id, Organization organization);
        Task<Organization?> DeleteAsync(int id);
    }
}