using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.CORE.Models.ViewModels;
using CBA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IService iserviceImplement;
        private readonly RoleManager<ApplicationRole> roleManager;


        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IService _iserviceImplement, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            iserviceImplement = _iserviceImplement;
        }
        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // LOGOUT ACTION

        [HttpPost]
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        // LOGIN ACTION

        [HttpGet]
        //[AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }
            
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    return RedirectToAction("index", "home");

                }
                ModelState.AddModelError(String.Empty, "Login attempt failed, please check that your details are correct");

            }
            return View(model);
        }


    }
}
