using HACS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace HACS.DynamicAuthorization
{
    public class DynamicAuthorizationHandler : AuthorizationHandler<DynamicAuthorizationRequirement>
    {
        private readonly ApplicationDBContext _context;
        public DynamicAuthorizationHandler(ApplicationDBContext context)
            => _context = context;

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, DynamicAuthorizationRequirement requirement)
        {
            var userRoles = context.User.Claims
                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                .Select(c => c.Value);

            var allowedRoles = await _context.EndpointRolePermissions
                .Where(e => e.Endpoint == requirement.EndpointName)
                .Select(e => e.Role)
                .ToListAsync();

            if (userRoles.Intersect(allowedRoles).Any())
                context.Succeed(requirement);
        }
    }
}
