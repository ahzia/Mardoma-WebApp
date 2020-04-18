using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchiveSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArchiveSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManger;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManger,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
        // GET: /<controller>/
        [HttpGet]
      public IActionResult Register() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
               var result=await  userManger.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                  await  signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
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
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }
                    ModelState.AddModelError(String.Empty, "Invalid Loogin Attempt");
                
            }
            return View(model);
        }
    }
}
