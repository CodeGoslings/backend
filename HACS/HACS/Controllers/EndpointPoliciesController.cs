using HACS.Data;
using HACS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(
            Summary = "Add role access to endpoint",
            Description = "Grants access permission for a specific role to access an endpoint."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Role access added successfully")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid endpoint or role")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "User is not authorized")]
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
        [SwaggerOperation(
            Summary = "Remove role access from endpoint",
            Description = "Removes access permission for a specific role from an endpoint."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Role access removed successfully")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Role access not found")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authenticated")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "User is not authorized")]
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