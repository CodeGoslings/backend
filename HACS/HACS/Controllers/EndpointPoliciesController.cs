using HACS.Data;
using HACS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HACS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class EndpointPoliciesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public EndpointPoliciesController(ApplicationDBContext context)
            => _context = context;

        [HttpPost("addRoleAccess")]
        public async Task<IActionResult> AddRoleAccess(string endpoint, string role)
        {
            if (string.IsNullOrWhiteSpace(endpoint) || string.IsNullOrWhiteSpace(role))
                return BadRequest();

            _context.EndpointRolePermissions.Add(new EndpointRolePermission
            {
                Endpoint = endpoint,
                Role = role
            });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("removeRoleAccess")]
        public async Task<IActionResult> RemoveRoleAccess(string endpoint, string role)
        {
            var record = await _context.EndpointRolePermissions
                .FirstOrDefaultAsync(e => e.Endpoint == endpoint && e.Role == role);

            if (record == null) return NotFound();

            _context.EndpointRolePermissions.Remove(record);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
