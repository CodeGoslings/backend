using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HACS.Controllers
{

    [Route("api/volunteer")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public VolunteerController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var volunteers = await _context.Volunteers.ToListAsync();
            var volunteersDto = volunteers.Select(v => v.ToVolunteerDto());
            return Ok(volunteersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var volunteer = await _context.Volunteers.FindAsync(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer.ToVolunteerDto());
        }


    }
}