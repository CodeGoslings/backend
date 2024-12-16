using HACS.Data;
using HACS.Interfaces.DonorManagement;
using HACS.Models.DonorManagement;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers.DonorManagement;

[Route("api/donor")]
[ApiController]
public class DonorController : ControllerBase
{
    private readonly IRepository<Donor> _donorRepo;
    private readonly IRepository<Donation> _donationRepo;
    
    public DonorController(IRepository<Donor> donorRepo, IRepository<Donation> donationRepo)
    {
        _donorRepo = donorRepo;
        _donationRepo = donationRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var donors = await _donorRepo.GetAllAsync();
        return Ok(donors);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var donor = await _donorRepo.GetByIdAsync(id);
        
        if (donor == null)
        {
            return NotFound();
        }
        return Ok(donor);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Donor donor)
    {
        await _donorRepo.CreateAsync(donor);
        return CreatedAtAction(nameof(GetById), new { id = donor.Id }, donor);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Donor donor)
    {
        var existingDonor = await _donorRepo.GetByIdAsync(donor.Id);
        if (existingDonor is null) return NotFound();
        
        foreach (var donation in existingDonor.DonationHistory.ToList())
        {
            if (!donor.DonationHistory.ToList().Contains(donation))
            { 
                await _donationRepo.DeleteAsync(donation.Id);
            }
        }
        foreach (var donation in donor.DonationHistory.ToList())
        { 
            if (!existingDonor.DonationHistory.ToList().Contains(donation))
            {
                await _donationRepo.CreateAsync(donation);
            }
        }

        await _donorRepo.UpdateAsync(donor);
        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var donor = await _donorRepo.GetByIdAsync(id);
        if (donor is null) return NotFound();
        foreach (var donation in donor.DonationHistory.ToList())
        {
            await _donationRepo.DeleteAsync(donation.Id);
        }
        
        await _donorRepo.DeleteAsync(id);
        return NoContent();
    }
}