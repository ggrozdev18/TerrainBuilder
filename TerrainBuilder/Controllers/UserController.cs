using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using TerrainBuilder.Data;

namespace TerrainBuilder.Controllers
{
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

      

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager
           )
        {
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateRole()
        {

            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Administrator"
            });

            return Ok();

        }
    }
}
