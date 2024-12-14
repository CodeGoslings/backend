using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Dtos.Assignment;
using HACS.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var assignments = _context.Assignments.ToList()
            .Select(a => a.ToAssignmentDto());
            return Ok(assignments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var assignment = _context.Assignments.Find(id);

            if (assignment == null)
            {
                return NotFound();
            }
            return Ok(assignment.ToAssignmentDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAssignmentRequestDto assignmentDto)
        {
            var assignmentModel = assignmentDto.ToAssignmentFromCreateDto();
            _context.Assignments.Add(assignmentModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = assignmentModel.Id }, assignmentModel.ToAssignmentDto());
        }

    }
}