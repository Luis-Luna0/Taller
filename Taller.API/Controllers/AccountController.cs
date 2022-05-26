using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taller.API.Datos;
using Taller.API.Datos.Entidades;
using Taller.API.Helpers;
using Taller.API.Models;
//using Taller.Common.Enums;
//using Taller.Common.Models;

namespace Taller.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        //    private readonly ICombosHelper _combosHelper;
        //    private readonly IBlobHelper _blobHelper;
        //    private readonly IMailHelper _mailHelper;

        public AccountController(IUserHelper userHelper)//, DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            // _context = context;
            //_combosHelper = combosHelper;
            //_blobHelper = blobHelper;
            // this._mailHelper = mailHelper;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            return View(new LoginViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }

            return View(model);
        }

        //metodo para desloguearce
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}