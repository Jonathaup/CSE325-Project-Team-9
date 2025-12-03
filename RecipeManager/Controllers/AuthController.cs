using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeManager.Services;
using System.Security.Claims;

namespace RecipeManager.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password, [FromForm] string? returnUrl)
        {
            var user = await _userService.ValidateCredentialsAsync(email, password);
            
            if (user == null) 
            {
                return Unauthorized("Invalid credentials."); 
            }

            await SignInAsync(user);
            return LocalRedirect(returnUrl ?? "/list"); // Changed default to /list
        }

        // --- UPDATED REGISTER METHOD ---
        [HttpPost("register")]
        [AllowAnonymous]
        // changed from [FromBody] to [FromForm] individual parameters
        public async Task<IActionResult> Register([FromForm] string email, [FromForm] string displayName, [FromForm] string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return BadRequest("Email and password are required.");

            try
            {
                var user = await _userService.RegisterAsync(email, displayName, password);
                
                // Automatically sign the user in after registration
                await SignInAsync(user);
                
                // Redirect to the recipes list
                return LocalRedirect("/list");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        private async Task SignInAsync(RecipeManager.Models.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.DisplayName ?? user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true }
            );
        }
    }
}