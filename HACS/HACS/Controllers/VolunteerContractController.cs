using HACS.Dtos.VolunteerContract;
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
    [Route("api/volunteer-contract")]
    [ApiController]
    [Authorize(Policy = "api/volunteer-contract")]
    public class VolunteerContractController : ControllerBase
    {
        private readonly IVolunteerRepository _volunteerRepo;
        private readonly IOrganizationRepository _organizationRepo;
        private readonly IVolunteerContractRepository _volunteerContractRepo;
        public VolunteerContractController(
            IVolunteerRepository volunteerRepo,
            IOrganizationRepository organizationRepo,
            IVolunteerContractRepository volunteerContractRepo
        )
        {
            _volunteerRepo = volunteerRepo;
            _organizationRepo = organizationRepo;
            _volunteerContractRepo = volunteerContractRepo;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var contract = await _volunteerContractRepo.GetByIdAsync(id);
            if (contract == null)
            {
                return NotFound("Volunteer contract does not exist");
            }
            return Ok(contract.ToVolunteerContractDto());
        }
        [HttpGet("volunteer/{volunteerId}")]
        public async Task<IActionResult> GetByVolunteer([FromRoute] int volunteerId)
        {
            var volunteer = await _volunteerRepo.GetByIdAsync(volunteerId);
            if (volunteer == null)
            {
                return BadRequest("Volunteer does not exist");
            }
            var volunteerContracts = await _volunteerContractRepo.GetByVolunteer(volunteerId);
            var volunteerContractsDto = volunteerContracts.Select(x => x.ToVolunteerContractDto()).ToList();
            return Ok(volunteerContractsDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVolunteerContractDto volunteerContractDto)
        {
            var contract = volunteerContractDto.ToVolunteerContractFromCreate();
            await _volunteerContractRepo.CreateAsync(contract);
            return CreatedAtAction(nameof(GetById), new { id = contract.Id }, contract.ToVolunteerContractDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateVolunteerContractDto volunteerContractDto)
        {
            var model = await _volunteerContractRepo.UpdateAsync(id, volunteerContractDto.ToVolunteerContractFromUpdate());

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model.ToVolunteerContractDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var contract = await _volunteerContractRepo.DeleteAsync(id);

            if (contract == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}