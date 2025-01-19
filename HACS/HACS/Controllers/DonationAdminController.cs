using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HACS.Dtos;
using HACS.Dtos.DonationAdmin;
using HACS.Helpers;
using HACS.Interfaces;
using HACS.Mappers;
using HACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HACS.Controllers;

[Route("api/donation-admin")]
[ApiController]
public class DonationAdminController : ControllerBase
{
    private readonly IRepository<DonationAdmin> _donationAdminRepo;
    private readonly IRepository<Donation> _donationRepo;
    private readonly UserManager<DonationAdmin> _userManager;
    private readonly IConfiguration _config;

    public DonationAdminController(
        IConfiguration config, 
        IRepository<DonationAdmin> donationAdminRepo, 
        IRepository<Donation> donationRepo,
        UserManager<DonationAdmin> userManager)
    {
        _donationAdminRepo = donationAdminRepo;
        _donationRepo = donationRepo;
        _userManager = userManager;
        _config = config;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password) || user.UserName is null) return Unauthorized();
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id),
            new (JwtRegisteredClaimNames.UniqueName, user.UserName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _config["JWT:ValidIssuer"],
            audience: _config["JWT:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);
        
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
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
        var dbDonationAdmin = await _donationAdminRepo.CreateAsync(donationAdmin, donationAdminDto.Password);
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