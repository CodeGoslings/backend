using HACS.Dtos.DonationAdmin;
using HACS.Interfaces;
using HACS.Mappers;
using HACS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers;

[Route("api/donation-admin")]
[ApiController]
public class DonationAdminController : ControllerBase
{
    private readonly IRepository<DonationAdmin> _donationAdminRepo;

    public DonationAdminController(IRepository<DonationAdmin> donationAdminRepo)
    {
        _donationAdminRepo = donationAdminRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var donationAdmins = await _donationAdminRepo.GetAllAsync();
        var donationAdminDtos = donationAdmins.Select(x => x.Map());
        return Ok(donationAdminDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var donationAdmin = await _donationAdminRepo.GetByIdAsync(id);
        if (donationAdmin == null) return NotFound();
        var donationAdminDto = donationAdmin.Map();
        return Ok(donationAdminDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDonationAdminDto donationAdminDto)
    {
        var donationAdmin = donationAdminDto.Map();
        var dbDonationAdmin = await _donationAdminRepo.CreateAsync(donationAdmin);
        return CreatedAtAction(nameof(GetById), new { id = dbDonationAdmin.Id }, dbDonationAdmin.Map());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] PostDonationAdminDto donationAdminDto, [FromRoute] Guid id)
    {
        var donationAdmin = donationAdminDto.Map(id);
        await _donationAdminRepo.UpdateAsync(donationAdmin);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _donationAdminRepo.DeleteAsync(id);
        return NoContent();
    }
}