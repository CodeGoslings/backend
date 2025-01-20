using HACS.Dtos.Donation;
using HACS.Dtos.Donor;
using HACS.Helpers;
using HACS.Interfaces;
using HACS.Mappers;
using HACS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HACS.Controllers;

[Route("api/donation")]
[ApiController]

public class DonationController : ControllerBase
{
    private readonly IRepository<DonationAdmin> _donationAdminRepo;
    private readonly IRepository<Donation> _donationRepo;
    private readonly IRepository<Donor> _donorRepo;
    
    public DonationController(IRepository<Donation> donationRepo, IRepository<DonationAdmin> donationAdminRepo, IRepository<Donor> donorRepo)
    {
        _donationAdminRepo = donationAdminRepo;
        _donationRepo = donationRepo;
        _donorRepo = donorRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var donationUser = HttpContext.User;
        if (donationUser.Identity is null) return Unauthorized();
        var donationAdmin = (await _donationAdminRepo.GetAllAsync()).FirstOrDefault(x => x.UserName == donationUser.Identity.Name);
        var donor = (await _donorRepo.GetAllAsync()).FirstOrDefault(x => x.UserName == donationUser.Identity.Name);
        if (donationAdmin is null && donor is null) return NotFound();
        if (donationAdmin is not null && donor is not null) return BadRequest();
        var donations = donor is not null ? donor.DonationHistory : (await _donationRepo.GetAllAsync()).Where(x => x.Status == DonationStatus.Pending).ToList();
        var donationDto = donations.Select(donation => donation.Map());
        return Ok(donationDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var donation = await _donationRepo.GetByIdAsync(id);
        if (donation == null) return NotFound();

        var donationDto = donation.Map();
        return Ok(donationDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PostDonationDto donationDto)
    {
        var donationUser = HttpContext.User;
        if (donationUser.Identity is null) return Unauthorized();
        var user = (await _donorRepo.GetAllAsync()).FirstOrDefault(x => x.UserName == donationUser.Identity.Name);
        if (user == null) return NotFound();
        
        var donation = donationDto.Map();
        var dbDonation = await _donationRepo.CreateAsync(donation, user.Id);
        var pdf = await PdfHelper.GenerateDonationConfirmation(dbDonation, _donorRepo);
        return File(pdf, "application/pdf", $"{dbDonation.Id}.pdf");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] PostDonationDto donationDto, [FromRoute] Guid id)
    {
        var donationUser = HttpContext.User;
        if (donationUser.Identity is null) return Unauthorized();
        var user = (await _donationAdminRepo.GetAllAsync()).FirstOrDefault(x => x.UserName == donationUser.Identity.Name);
        if (user == null) return NotFound();
        var donation = donationDto.Map(id);
        await _donationRepo.UpdateAsync(donation);
        // user.ReviewedDonations.Add(donation);
        await _donationAdminRepo.UpdateAsync(user);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _donationRepo.DeleteAsync(id);
        return NoContent();
    }
    
    [HttpGet("donation-report/{year:int}")]
    public async Task<IActionResult> GetDonationReport(int year)
    {
        var donationUser = HttpContext.User;
        if (donationUser.Identity is null) return Unauthorized();
        var user = (await _donationAdminRepo.GetAllAsync()).FirstOrDefault(x => x.UserName == donationUser.Identity.Name);
        if (user == null) return NotFound();
        
        var donations = await _donationRepo.GetAllAsync();
        var file = PdfHelper.GenerateDonationsReport(donations, year);
        return File(file, "application/pdf", $"Report_{year}.pdf");
    }
}