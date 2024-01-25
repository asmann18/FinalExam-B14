using FinalExam_B14.Areas.Admin.ViewModels.AccountVMs;
using FinalExam_B14.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalExam_B14.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser user = new()
            {
                UserName = vm.Username,
                Email = vm.Email,

            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }


            await _userManager.AddToRoleAsync(user, "Admin");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var user = await _userManager.FindByNameAsync(vm.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Password or username is wrong");
                return View(vm);
            }


            var succedd = await _signInManager.PasswordSignInAsync(user, vm.Password, true, true);
            if (!succedd.Succeeded)
            {
                ModelState.AddModelError("", "Password or username is wrong");
                return View(vm);
            }


            var role = await _userManager.GetRolesAsync(user);
            if (role.FirstOrDefault() == "Admin")
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    IdentityRole role1 = new IdentityRole() { Name = "Admin" };
        //    IdentityRole role2 = new IdentityRole() { Name = "Member" };

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    return RedirectToAction("Index", "Dashboard");
        //}
    }
}
