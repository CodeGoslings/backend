using HACS.Dtos.Donor;
using HACS.Interfaces;
using HACS.Mappers;
using HACS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers;

[Route("api/donor")]
[ApiController]
public class DonorController : ControllerBase
{
    private readonly IRepository<Donor> _donorRepo;
    
    public DonorController(IRepository<Donor> donorRepo)
    {
        _donorRepo = donorRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var donors = await _donorRepo.GetAllAsync();
        var donorDtos = donors.Select(donor => donor.Map());
        return Ok(donorDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var donor = await _donorRepo.GetByIdAsync(id);
        if (donor == null) return NotFound();
        var donorDto = donor.Map();
        return Ok(donorDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDonorDto donorDto)
    {
        var donor = donorDto.Map();
        var dbDonor = await _donorRepo.CreateAsync(donor);
        return CreatedAtAction(nameof(GetById), new { id = dbDonor.Id }, dbDonor.Map());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] PostDonorDto donorDto, [FromRoute] Guid id)
    {
        var donor = donorDto.Map(id);
        await _donorRepo.UpdateAsync(donor);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _donorRepo.DeleteAsync(id);
        return NoContent();
    }
}