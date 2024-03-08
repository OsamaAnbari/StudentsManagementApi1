using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Students_Management_Api.Models;
using Students_Management_Api.Services;

namespace Students_Management_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IConfiguration configuraion, 
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuraion;
            _logger = logger;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByNameAsync(model.Username);
                var roles = await _userManager.GetRolesAsync(user);

                AuthService userService = new AuthService(_configuration);
                var token = userService.GenerateJwtToken(roles, user.Id);

                _logger.LogInformation("Admin Logged In");
                //return Ok(new { message = $"{model.Username} User is Logged in" });
                return Ok(new { token });
            }

            return BadRequest(new { error = "Invalid login attempt." });
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (!roleExists)
            {
                return BadRequest($"Role '{model.Role}' does not exist");
            }

            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(user, model.Role);

                if (addToRoleResult.Succeeded)
                {
                    return Ok($"User '{model.UserName}' created successfully and assigned to the role '{model.Role}'");
                }
                else
                {
                    // If adding to role fails, delete the user to maintain consistency
                    await _userManager.DeleteAsync(user);
                    return BadRequest($"Failed to assign the user to the role: {string.Join(", ", addToRoleResult.Errors)}");
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
