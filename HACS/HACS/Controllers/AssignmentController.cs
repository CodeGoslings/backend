using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Dtos.Assignment;
using HACS.Interfaces;
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
        private readonly IAssignmentRepository _assignmentRepo;
        private readonly IVolunteerRepository _volunteerRepo;
        public AssignmentController(ApplicationDBContext context, IAssignmentRepository assignmentRepo, IVolunteerRepository volunteerRepo)
        {
            _context = context;
            _assignmentRepo = assignmentRepo;
            _volunteerRepo = volunteerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _assignmentRepo.GetAllAsync();
            var assignmentsDto = assignments.Select(a => a.ToAssignmentDto());
            return Ok(assignmentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);

            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment.ToAssignmentDto());
        }

        [HttpPost("{volunteerId}")]
        public async Task<IActionResult> Create([FromRoute] int volunteerId, [FromBody] CreateAssignmentDto assignmentDto)
        {
            if (!await _volunteerRepo.ExistsAsync(volunteerId))
            {
                return BadRequest("Volunteer does not exist");
            }
            var assignmentModel = assignmentDto.ToAssignmentFromCreateDto(volunteerId);
            await _assignmentRepo.CreateAsync(assignmentModel);

            return CreatedAtAction(nameof(GetById), new { id = assignmentModel.Id }, assignmentModel.ToAssignmentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAssignmentDto updateDto)
        {
            var assignmentModel = await _assignmentRepo.UpdateAsync(id, updateDto);

            if (assignmentModel == null)
            {
                return NotFound();
            }

            return Ok(assignmentModel.ToAssignmentDto());
        }

    }
}