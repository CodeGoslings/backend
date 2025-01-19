using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HACS.Dtos;
using HACS.Dtos.Donor;
using HACS.Interfaces;
using HACS.Mappers;
using HACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HACS.Controllers;

[Route("api/donor")]
[ApiController]
public class DonorController : ControllerBase
{
    private readonly IRepository<Donor> _donorRepo;
    private readonly UserManager<DonationAdmin> _userManager;
    private readonly IConfiguration _config;
    
    public DonorController(IConfiguration config, IRepository<Donor> donorRepo, UserManager<DonationAdmin> userManager)
    {
        _donorRepo = donorRepo;
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
        var dbDonor = await _donorRepo.CreateAsync(donor, donorDto.Password);
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