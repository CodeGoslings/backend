using HACS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HACS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("initRoles")]
        [SwaggerOperation(Summary = "Initialize roles", Description = "Creates Admin, Volunteer, and OrganizationManager roles if they do not exist." +
            "Ensure that roles are created before registration")]
        [SwaggerResponse(StatusCodes.Status200OK, "Roles created or already exist.")]
        public async Task<IActionResult> InitRoles()
        {
            var roles = new[] { "Admin", "Volunteer", "OrganizationManager" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            return Ok(new { Status = "Success", Message = "Roles created or already exist." });
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register a new user", Description = "Registers a user and assigns a specified role.")]
        [SwaggerResponse(StatusCodes.Status200OK, "User created successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid role or user already exists.")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var validRoles = new[] { "Admin", "Volunteer", "OrganizationManager" };
            if (!validRoles.Contains(model.Role))
            {
                return BadRequest(new { Status = "Error", Message = "Invalid role requested" });
            }

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest(new { Status = "Error", Message = "User already exists!" });
            }

            var user = new IdentityUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { Status = "Error", Message = "User creation failed!", Errors = result.Errors });
            }

            await _userManager.AddToRoleAsync(user, model.Role);
            return Ok(new { Status = "Success", Message = $"User created with role {model.Role} successfully!" });
        }

        [HttpPost]
        [Route("login")]
        [SwaggerOperation(Summary = "User login", Description = "Logs in a user and returns a JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Token generated successfully.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User unauthorized.")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Volunteer,OrganizationManager")]
        [Route("authorizedHelloWorld")]
        [SwaggerOperation(Summary = "Authorized HelloWorld", Description = "A test which returns a message if user is authorized.")]
        [SwaggerResponse(StatusCodes.Status200OK, "You are authorized.")]
        public async Task<IActionResult> AuthorizedHelloWorld()
        {
            return Ok("You are authorized");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
