using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taller.API.Datos;
using Microsoft.EntityFrameworkCore;

namespace Taller.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;


        public UsersController(DataContext context)
        {
            _context = context; 
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
                .Include(x => x.DocumentType)
                .ToListAsync());
        }
    }
}
