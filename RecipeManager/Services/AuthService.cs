using Microsoft.AspNetCore.Identity;


namespace RecipeManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<bool> RegisterAsync(string username, string password)
        {
            var user = new IdentityUser { UserName = username, Email = username };
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }


        public async Task<bool> LoginAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result.Succeeded;
        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}