using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Dtos.Assignment;
using HACS.Models;

namespace HACS.Interfaces
{
    public interface IAssignmentRepository
    {
        Task<List<Assignment>> GetAllAsync();
        Task<Assignment?> GetByIdAsync(int id);
        Task<Assignment> CreateAsync(Assignment assignment);
        Task<Assignment?> UpdateAsync(int id, Assignment assignment);
        Task<Assignment?> DeleteAsync(int id);
    }
}