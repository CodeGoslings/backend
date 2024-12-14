using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            var volunteers = _context.Volunteers.ToList()
                .Select(v => v.ToVolunteerDto());
            return Ok(volunteers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var volunteer = _context.Volunteers.Find(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer.ToVolunteerDto());
        }


    }
}