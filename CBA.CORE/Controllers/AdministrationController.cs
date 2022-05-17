using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.CORE.Models.ViewModels;
using CBA.DATA;
using CBA.Core.Enums;
using CBA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IService iserviceImplement;
        private readonly RoleManager<ApplicationRole> roleManager;


        public AdministrationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IService _iserviceImplement, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            iserviceImplement = _iserviceImplement;
        }

        public new IActionResult NotFound()
        {
            return View();
        }

        // ADD ACTION

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                //copy the data in AddUserViewModel into ApplicationUser
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = iserviceImplement.GenerateUserName(model.FirstName, model.LastName),
                    Gender = model.Gender,
                };

                //call service implementation to generate random password and then hash
                var password = iserviceImplement.GeneratePassword();
                var passHasher = new PasswordHasher<ApplicationUser>();
                var hashed = passHasher.HashPassword(user, password);
                user.PasswordHash = hashed;

                var result = await userManager.CreateAsync(user, hashed);

                var mail = new MailRequest
                {
                    ToEmail = model.Email,
                    Subject = model.LastName,
                    Body = password,
                };

                // On succesful login redirect to home page
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    await iserviceImplement.SendEmailAsync(mail);
                    return RedirectToAction("listusers", "administration");
                }

                // If there are any errors, add them to the ModelState object
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
     
        public IActionResult ListUsers()
        {
            var users = userManager.Users;

            //foreach(var user in users)
            //{
            //    if(user.UserState == UserState.Disabled)
            //    {
            //        var lockoutEndDate = new DateTime(2999, 01, 01);
            //        await userManager.SetLockoutEnabledAsync(user, true);
            //        await userManager.SetLockoutEndDateAsync(user, lockoutEndDate);
            //    }
            //}
            return View(users);
        }

        // EDIT USER ACTION
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if(user == null)
            {

                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");

            }

            var editUser = new EditUserViewModel
            {
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName= user.LastName,
                Gender = user.Gender,
                Email = user.Email,
                Id = user.Id,
                UserState = user.UserState,
            };

            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);


                user.UserName = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Gender = model.Gender;
                user.Email = model.Email;
                user.UserState = model.UserState;

                var result = await userManager.UpdateAsync(user);

                if(user.UserState == UserState.Disabled)
                {
                    var lockoutEndDate = new DateTime(2999, 01, 01);
                    await userManager.SetLockoutEnabledAsync(user, true);
                    await userManager.SetLockoutEndDateAsync(user, lockoutEndDate);
                }
                else
                {
                    await userManager.SetLockoutEnabledAsync(user, false);
                }

                // On succesful login redirect to home page
                if (result.Succeeded)
                {
                    return RedirectToAction("listusers", "administration");
                }

                // If there are any errors, add them to the ModelState object
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DetailUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            DetailsUserViewModel detailsUserViewModel = new()
            {
                User = await userManager.FindByIdAsync(id),
                PageTitle = "Details View",
                Roles = (List<string>)await userManager.GetRolesAsync(user),
            };
            return View(detailsUserViewModel);
        }


        // ADD ROLES ACTION
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await roleManager.CreateAsync(applicationRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("listroles", "administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // LIST ROLES ACTION


        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        // EDIT ROLES ACTION
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var roleClaims = await roleManager.GetClaimsAsync(role);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                State = role.State,
                Claims = roleClaims.Select(rc => rc.Value).ToList(),
            };

            foreach (var user in userManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await userManager.IsInRoleAsync(user, role.Name) && role.State == State.Enabled)
                {

                    model.Users.Add(user.UserName);
                }
                
               
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditDetail(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            EditDetailRoleViewModel editDetailRoleViewModel = new()
            {
                RoleName = role.Name,
                State = role.State,
                Users = (List<Claim>)await roleManager.GetClaimsAsync(role),

            };

            return View(editDetailRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            List<ApplicationUser> users = (List<ApplicationUser>)userManager.Users;

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                role.State = model.State;
                //foreach(var user in users)
                //{
                //    if(role.State == State.Disabled)
                //    {
                //        await userManager.RemoveFromRoleAsync(user, role.Name);
                //    }
                //}

                //Update role in database
                var result = await roleManager.UpdateAsync(role);                

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        //EDIT USER ROLE SYNC AXTION
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string id)
        {
            ViewBag.roleid = id;

            var role = await roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"The Role with id: {id} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName

                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            for(int i = 0; i < model.Count(); i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Id)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Id)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if(i < model.Count() - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole", new { id });
                    }
                }
            }
            return RedirectToAction("EditRole", new { id });
        }

        //MANAGE ROLES FOR A USER
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string Id)
        {
            ViewBag.userId = Id;

            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var model = new List<InUserRolesViewModel>();

            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new InUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>
        ManageUserRoles(List<InUserRolesViewModel> model, string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {Id} cannot be found";
                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id });
        }

        //CLAIMS
       
        [HttpGet]
        public async Task<IActionResult> ManageRoleClaims(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            var existingRoleClaims = await roleManager.GetClaimsAsync(role);
            var model = new RoleClaimsViewModel
            {
                //Id = role.Id,
                Id = id
            };
            foreach (Claim claim in ClaimsStore.AllClaims)
            {
                RoleClaim roleClaim = new RoleClaim
                {
                    ClaimType = claim.Type
                };
                if (existingRoleClaims.Any(c => c.Type == claim.Type))
                {
                    roleClaim.IsSelected = true;
                }
                model.Cliams.Add(roleClaim);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoleClaims(RoleClaimsViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            var claims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.Cliams.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType));
            foreach (var claim in selectedClaims)
            {
                await roleManager.AddClaimAsync(role, claim);
            }
            return RedirectToAction("listroles");
        }
    }
}
