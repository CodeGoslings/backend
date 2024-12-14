using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Dtos.Assignment;
using HACS.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HACS.Controllers
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public AssignmentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _context.Assignments.ToListAsync();
            var assignmentsDto = assignments.Select(a => a.ToAssignmentDto());
            return Ok(assignmentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment.ToAssignmentDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAssignmentRequestDto assignmentDto)
        {
            var assignmentModel = assignmentDto.ToAssignmentFromCreateDto();
            await _context.Assignments.AddAsync(assignmentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = assignmentModel.Id }, assignmentModel.ToAssignmentDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAssignmentRequestDto updateDto)
        {
            var assignmentModel = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == id);
            if (assignmentModel == null)
            {
                return NotFound();
            }

            assignmentModel.Description = updateDto.Description;
            assignmentModel.DueDate = updateDto.DueDate;
            assignmentModel.VolunteerId = updateDto.VolunteerId;
            await _context.SaveChangesAsync();

            return Ok(assignmentModel.ToAssignmentDto());
        }

    }
}