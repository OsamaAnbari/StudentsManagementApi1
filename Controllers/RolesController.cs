using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RoleViewModel Role)
        {
            if (string.IsNullOrWhiteSpace(Role.RoleName))
            {
                return BadRequest("Role name cannot be empty");
            }

            var roleExists = await _roleManager.RoleExistsAsync(Role.RoleName);
            if (roleExists)
            {
                return BadRequest("Role already exists");
            }

            var role = new IdentityRole(Role.RoleName);

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return Ok(new { message = $"\"Role '{Role.RoleName}' created successfully, its ID is {role.Id}\"" });
            }

            return BadRequest(new { message = $"Failed to create role: {string.Join(", ", result.Errors)}" });
        }
    }
}
