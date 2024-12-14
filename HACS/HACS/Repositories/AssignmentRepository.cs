using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Dtos.Assignment;
using HACS.Interfaces;
using HACS.Models;
using Microsoft.EntityFrameworkCore;

namespace HACS.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDBContext _context;
        public AssignmentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Assignment> CreateAsync(Assignment assignment)
        {
            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<Assignment?> DeleteAsync(int id)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == id);

            if (assignment == null)
            {
                return null;
            }
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<List<Assignment>> GetAllAsync()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task<Assignment?> GetByIdAsync(int id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        public async Task<Assignment?> UpdateAsync(int id, UpdateAssignmentRequestDto assignmentDto)
        {
            var existingAssignment = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingAssignment == null)
            {
                return null;
            }

            existingAssignment.Description = assignmentDto.Description;
            existingAssignment.DueDate = assignmentDto.DueDate;
            existingAssignment.VolunteerId = assignmentDto.VolunteerId;

            await _context.SaveChangesAsync();
            return existingAssignment;
        }
    }
}