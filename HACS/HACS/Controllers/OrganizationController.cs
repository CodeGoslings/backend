using HACS.Dtos.Organization;
using HACS.Interfaces;
using HACS.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACS.Controllers
{
    [Route("api/organization")]
    [ApiController]
    [Authorize(Policy = "api/organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepo;
        public OrganizationController(IOrganizationRepository organizationRepo)
        {
            _organizationRepo = organizationRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _organizationRepo.GetAllAsync();
            var organizationsDto = organizations.Select(a => a.ToOrganizationDto());
            return Ok(organizationsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var organization = await _organizationRepo.GetByIdAsync(id);

            if (organization == null)
            {
                return NotFound();
            }
            return Ok(organization.ToOrganizationDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationDto organizationDto)
        {
            var organizationModel = organizationDto.ToOrganizationFromCreate();
            await _organizationRepo.CreateAsync(organizationModel);

            return CreatedAtAction(nameof(GetById), new { id = organizationModel.Id }, organizationModel.ToOrganizationDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrganizationDto organizationDto)
        {
            var organizationModel = await _organizationRepo.UpdateAsync(id, organizationDto.ToOrganizationFromUpdate());

            if (organizationModel == null)
            {
                return NotFound();
            }

            return Ok(organizationModel.ToOrganizationDto());
        }
    }
}