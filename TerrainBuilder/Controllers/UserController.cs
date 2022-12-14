using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TerrainBuilder.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace TerrainBuilder.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;



        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            ApplicationDbContext _context   
           )
        {
            roleManager = _roleManager;
            userManager = _userManager;
            context= _context;
        }
        public IActionResult Index()
        {
            List<string> users = context.Users.Select(m => m.UserName).ToList();
            ViewBag.users = users;
            return View();
        }
        //public async Task<IActionResult> CreateRole()
        //{

        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "User"
        //    });

        //    return Ok();

        //}

        public async Task<IActionResult> AssignRole(string userId,string role)
        {
            // userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser appUser = await context.Users.Where(m => m.UserName == userId).FirstOrDefaultAsync();
            List<string> roles = new  List<string>();
            roles.Add(role);
            
            await userManager.AddToRolesAsync(appUser, roles);
            return RedirectToAction(nameof(Index));
        }
    }
}
