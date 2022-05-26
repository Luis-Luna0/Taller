using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Taller.API.Datos;
using Taller.API.Datos.Entidades;
using Taller.API.Models;

namespace Taller.API.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly DataContext _context;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, DataContext context, SignInManager<User> signInManager)
        {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
        return await _userManager.CreateAsync(user, password);
        }
        
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }


        public async Task CheckRoleAsync(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = roleName });
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.Include(x => x.Document).FirstOrDefaultAsync(x=> x.Email== email);
        }

        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }
        // metodo para iniciar sesion
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }
        // metodo para salir de la sesion
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
