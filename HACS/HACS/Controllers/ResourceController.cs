using HACS.Dtos.Organization;
using HACS.Dtos.Resource;
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
    [Route("api/resource")]
    [ApiController]
    [Authorize(Policy = "api/resource")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceRepository _resourceRepo;
        private readonly IOrganizationRepository _organizationRepo;
        public ResourceController(IResourceRepository resourceRepo, IOrganizationRepository organizationRepo)
        {
            _resourceRepo = resourceRepo;
            _organizationRepo = organizationRepo;
        }

        [HttpGet("by-organization/{organizationId}")]
        public async Task<IActionResult> GetOrganizationResources([FromRoute] int organizationId)
        {
            var organization = await _organizationRepo.GetByIdAsync(organizationId);
            if (organization == null)
            {
                return NotFound();
            }
            var resources = await _resourceRepo.GetByOrganization(organizationId);
            var resourcesDto = resources.Select(a => a.ToResourceDto());
            return Ok(resourcesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var resource = await _resourceRepo.GetByIdAsync(id);

            if (resource == null)
            {
                return NotFound();
            }
            return Ok(resource.ToResourceDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateResourceDto resourceDto)
        {
            var resourceModel = resourceDto.ToResourceFromCreate();
            await _resourceRepo.CreateAsync(resourceModel);

            return CreatedAtAction(nameof(GetById), new { id = resourceModel.Id }, resourceModel.ToResourceDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateResourceDto resourceDto)
        {
            var resourceModel = await _resourceRepo.UpdateAsync(id, resourceDto.ToResourceFromUpdate());

            if (resourceModel == null)
            {
                return NotFound();
            }

            return Ok(resourceModel.ToResourceDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resource = await _resourceRepo.DeleteAsync(id);

            if (resource == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}