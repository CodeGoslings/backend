using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Models;

namespace HACS.Interfaces
{
    public interface IVolunteerContractRepository
    {
        Task<List<VolunteerContract>> GetAllAsync();
        Task<VolunteerContract?> GetByIdAsync(int id);
        Task<IEnumerable<VolunteerContract>> GetByVolunteer(int volunteerId);
        Task<IEnumerable<VolunteerContract>> GetByOrganization(int organizationId);
        Task<VolunteerContract> CreateAsync(VolunteerContract contract);
        Task<VolunteerContract?> UpdateAsync(int id, VolunteerContract contract);
        Task<VolunteerContract?> DeleteAsync(int id);
    }
}