using HACS.Data;
using HACS.Interfaces.DonorManagement;
using HACS.Models.DonorManagement;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers.DonorManagement;

[Route("api/donation-admin")]
[ApiController]
public class DonationAdminController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IRepository<DonationAdmin> _donationAdminRepo;
    
    public DonationAdminController(ApplicationDBContext context, IRepository<DonationAdmin> donationAdminRepo)
    {
        _context = context;
        _donationAdminRepo = donationAdminRepo;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var donationAdmins = await _donationAdminRepo.GetAllAsync();
        return Ok(donationAdmins);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var donationAdmin = await _donationAdminRepo.GetByIdAsync(id);
        
        if (donationAdmin == null)
        {
            return NotFound();
        }
        return Ok(donationAdmin);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DonationAdmin donationAdmin)
    {
        await _donationAdminRepo.CreateAsync(donationAdmin);
        return CreatedAtAction(nameof(GetById), new { id = donationAdmin.Id }, donationAdmin);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] DonationAdmin donationAdmin)
    {
        var existingDonationAdmin = await _donationAdminRepo.GetByIdAsync(donationAdmin.Id);
        if (existingDonationAdmin is null) return NotFound();

        await _donationAdminRepo.UpdateAsync(donationAdmin);
        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var donationAdmin = await _donationAdminRepo.GetByIdAsync(id);
        if (donationAdmin is null) return NotFound();
        
        await _donationAdminRepo.DeleteAsync(id);
        return NoContent();
    }
}