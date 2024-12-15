using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HACS.Interfaces;
using HACS.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers
{
    [Route("api/volunteer-contract")]
    [ApiController]
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
        [HttpGet("{volunteerId}")]
        public async Task<IActionResult> GetVolunteerOrganizations([FromRoute] int volunteerId)
        {
            var volunteer = await _volunteerRepo.GetByIdAsync(volunteerId);
            if (volunteer == null)
            {
                return BadRequest("Volunteer does not exist");
            }
            var volunteerContracts = await _volunteerContractRepo.GetByVolunteer(volunteerId);
            var organizations = volunteerContracts.Select(a => a.Organization).ToList();
            var organizationsDto = organizations.Select(a => a.ToOrganizationDto());
            return Ok(organizationsDto);
        }
    }
}