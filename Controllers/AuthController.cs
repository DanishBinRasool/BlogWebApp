using BlogApp.Data;
using BlogApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers
{
    public class AuthController(AppDbContext context,
                                UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                SignInManager<IdentityUser> signInManager) : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var existingUser = await context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                var existingUser = await userManager.FindByEmailAsync(model.Email);
                if (existingUser == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                    };

                    var result = await userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        var roleExists = await roleManager.RoleExistsAsync("User");
                        if (!roleExists)
                        {
                            var role = new IdentityRole("User");
                            await roleManager.CreateAsync(role);
                        }

                        await userManager.AddToRoleAsync(user, "User");
                        await signInManager.SignInAsync(user, isPersistent: true);
                        return RedirectToAction("Index", "Post");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);
                }
            }
                return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var signInResult = await signInManager.PasswordSignInAsync(user, model.Password,isPersistent:false, lockoutOnFailure: false);
                    if (signInResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: true);
                        return RedirectToAction("Index", "Post");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email or password is incorrect");
                        return View(model);
                    }
                }
                ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
                return View(model);
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Post");
        }
    }
}
