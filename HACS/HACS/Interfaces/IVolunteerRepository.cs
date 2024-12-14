using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Models;

namespace HACS.Interfaces
{
    public interface IVolunteerRepository
    {
        Task<List<Volunteer>> GetAllAsync();
        Task<Volunteer?> GetByIdAsync(int id);
        Task<Volunteer?> CreateAsync(Volunteer volunteer);
        Task<Volunteer?> UpdateAsync(int id, Volunteer volunteer);
        Task<Volunteer?> DeleteAsync(int id);
    }
}