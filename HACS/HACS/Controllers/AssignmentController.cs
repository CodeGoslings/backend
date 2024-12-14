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
        private readonly IAssignmentRepository _repository;
        public AssignmentController(ApplicationDBContext context, IAssignmentRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assignments = await _repository.GetAllAsync();
            var assignmentsDto = assignments.Select(a => a.ToAssignmentDto());
            return Ok(assignmentsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var assignment = await _repository.GetByIdAsync(id);

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
            await _repository.CreateAsync(assignmentModel);

            return CreatedAtAction(nameof(GetById), new { id = assignmentModel.Id }, assignmentModel.ToAssignmentDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAssignmentRequestDto updateDto)
        {
            var assignmentModel = await _repository.UpdateAsync(id, updateDto);

            if (assignmentModel == null)
            {
                return NotFound();
            }

            return Ok(assignmentModel.ToAssignmentDto());
        }

    }
}