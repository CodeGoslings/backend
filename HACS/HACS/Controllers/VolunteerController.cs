using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Data;
using HACS.Interfaces;
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
        private readonly IVolunteerRepository _repository;

        public VolunteerController(ApplicationDBContext context, IVolunteerRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var volunteers = await _repository.GetAllAsync();
            var volunteersDto = volunteers.Select(v => v.ToVolunteerDto());
            return Ok(volunteersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var volunteer = await _repository.GetByIdAsync(id);

            if (volunteer == null)
            {
                return NotFound();
            }

            return Ok(volunteer.ToVolunteerDto());
        }


    }
}