using HACS.Data;
using HACS.Interfaces.DonorManagement;
using HACS.Models.DonorManagement;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers.DonorManagement;

[Route("api/donor")]
[ApiController]
public class DonorController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IRepository<Donor> _donorRepo;
    private readonly IRepository<Donation> _donationRepo;
    
    public DonorController(ApplicationDBContext context, IRepository<Donor> donorRepo, IRepository<Donation> donationRepo)
    {
        _context = context;
        _donorRepo = donorRepo;
        _donationRepo = donationRepo;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var donors = await _donorRepo.GetAllAsync();
        return Ok(donors);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var donor = await _donorRepo.GetByIdAsync(id);
        
        if (donor == null)
        {
            return NotFound();
        }
        return Ok(donor);
    }
}